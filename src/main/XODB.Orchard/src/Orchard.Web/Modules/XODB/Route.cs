using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace XODB
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
                    Priority = 5,
                    Route = new Route(
                        "XODB/{controller}/{action}",
                        new RouteValueDictionary {
                            {"area", "XODB"},
                            {"controller", "User"},
                            {"action", "Index"}
                        },
                        new RouteValueDictionary {
                            {"area", "XODB"},
                            {"controller", "User"}
                        },
                        new RouteValueDictionary {
                            {"area", "XODB"}
                        },
                        new MvcRouteHandler())
                },
                 new RouteDescriptor {
                    Priority = 5,
                    Route = new Route(
                        "XODB/{controller}/{action}/{id}/{verb}",
                        new RouteValueDictionary {
                            {"area", "XODB"},
                            {"controller", "User"}                            
                        },
                        new RouteValueDictionary {
                            {"area", "XODB"},
                            {"controller", "User"},                          
                        },
                        new RouteValueDictionary {
                            {"area", "XODB"}
                        },
                        new MvcRouteHandler())
                }
            };
        }
    }
}