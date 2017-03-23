using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace Server.Commands
{
    public class IisSitesCommand
    {
        public IEnumerable<IisSite> Execute()
        {
            // Note. Importing Microsoft.Web.Administration version 10.0.0 (for .netstandard) gave issues 
            var manager = ServerManager.OpenRemote("localhost");
            return manager.Sites.Select(x => new IisSite()
            {
                Id = x.Id,
                Name = x.Name
            });
        }
    }
}