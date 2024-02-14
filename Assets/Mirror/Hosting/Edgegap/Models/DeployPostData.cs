using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mirror.Hosting.Edgegap.Models
{
    public struct DeployPostData
    {
        [JsonProperty("app_name")]
        public string AppName { get; set; }

        [JsonProperty("version_name")]
        public string AppVersionName { get; set; }

        [JsonProperty("ip_list")]
        public IList<string> IpList { get; set; }

        public DeployPostData(string appName, string appVersionName, IList<string> ipList)
        {
            AppName = appName;
            AppVersionName = appVersionName;
            IpList = ipList;
        }
    }
}
