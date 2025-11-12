using ST10070933_PROG7312_MunicipalServices.Models;
using ST10070933_PROG7312_MunicipalServices.DataStructures;
using ST10070933_PROG7312_MunicipalServices.Services.DataStructures;

namespace ST10070933_PROG7312_MunicipalServices.Services
{
    // Manages service requests using data structures for organization and retrieval.
    public class ServiceRequestManager
    {
        //  For organizing by Priority
        private AVLTree<ServiceRequest> _priorityTree;

        // For organizing by NumericId
        private AVLTree<ServiceRequest> _idTree;

        // MinHeap for urgent requests
        private MinHeap<(int Priority, ServiceRequest Request)> _urgentRequestsHeap;

        // Graph for location routing
        private Graph _locationGraph;

        public ServiceRequestManager()
        {
            _priorityTree = new AVLTree<ServiceRequest>();
            _idTree = new AVLTree<ServiceRequest>();
            _urgentRequestsHeap = new MinHeap<(int, ServiceRequest)>();
            _locationGraph = new Graph();

            // Sample locations in graph
            InitializeLocationGraph();
        }

        private void InitializeLocationGraph()
        {
            // Add nodes (locations/departments)
            _locationGraph.AddNode("Central Office");
            _locationGraph.AddNode("Water Department");
            _locationGraph.AddNode("Electrical Department");
            _locationGraph.AddNode("Roads Department");
            _locationGraph.AddNode("Waste Management");
            _locationGraph.AddNode("Parks Department");

            // Add edges with weights (distances/time in minutes)
            _locationGraph.AddEdge("Central Office", "Water Department", 10);
            _locationGraph.AddEdge("Central Office", "Electrical Department", 15);
            _locationGraph.AddEdge("Central Office", "Roads Department", 12);
            _locationGraph.AddEdge("Water Department", "Waste Management", 8);
            _locationGraph.AddEdge("Electrical Department", "Parks Department", 20);
            _locationGraph.AddEdge("Roads Department", "Waste Management", 7);
            _locationGraph.AddEdge("Waste Management", "Parks Department", 10);
        }

        public void AddRequest(ServiceRequest request)
        {          
            _priorityTree.Insert(request.Priority, request);
            _idTree.Insert(request.NumericId, request);

            // Add high priority requests to heap (High priority)
            if (request.Priority == 3)
            {
                // Negative priority for min heap (higher priority = lower number)
                _urgentRequestsHeap.Add((-request.Priority, request));
            }
        }

        // Searches for a service request by its numeric ID using AVL tree.
        public ServiceRequest SearchById(int numericId)
        {
            return _idTree.Search(numericId);
        }

        // Retrieves all service requests sorted by priority level.
        public List<ServiceRequest> GetAllRequestsSortedByPriority()
        {
            return _priorityTree.InOrderTraversal();
        }

        // Retrieves all service requests sorted by numeric ID.
        public List<ServiceRequest> GetAllRequestsSortedById()
        {
            return _idTree.InOrderTraversal();
        }

        // Retrieves all urgent (high priority) requests from the heap.
        public List<ServiceRequest> GetUrgentRequests()
        {
            var urgentList = new List<ServiceRequest>();
            var tempHeap = new MinHeap<(int, ServiceRequest)>();

            // Extract all from heap
            while (_urgentRequestsHeap.Count > 0)
            {
                var item = _urgentRequestsHeap.Pop();
                urgentList.Add(item.Request);
                tempHeap.Add(item);
            }

            // Restore heap
            while (tempHeap.Count > 0)
            {
                _urgentRequestsHeap.Add(tempHeap.Pop());
            }

            return urgentList;
        }

        public Dictionary<string, int> GetShortestPathFromCentralOffice(string destination)
        {
            return GraphAlgorithms.Dijkstra(_locationGraph, "Central Office");
        }

        public List<string> GetDepartmentTraversal(string startLocation)
        {
            return GraphAlgorithms.DepthFirstTraversal(_locationGraph, startLocation);
        }

        public List<(string, string, int)> GetOptimalRoutingNetwork()
        {
            return GraphAlgorithms.MinimumSpanningTree(_locationGraph);
        }

        public Graph GetLocationGraph()
        {
            return _locationGraph;
        }
    }
}