using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Mirror.Hosting.Edgegap.Models.SDK {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class CustomSessionModel {
    /// <summary>
    /// The List of IP of your user, Array of String, example:     [\"162.254.103.13\",\"198.12.116.39\", \"162.254.135.39\", \"162.254.129.34\"]
    /// </summary>
    /// <value>The List of IP of your user, Array of String, example:     [\"162.254.103.13\",\"198.12.116.39\", \"162.254.135.39\", \"162.254.129.34\"]</value>
    [DataMember(Name="ip_list", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ip_list")]
    public List<string> IpList { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      StringBuilder sb = new StringBuilder();
      sb.Append("class CustomSessionModel {\n");
      sb.Append("  IpList: ").Append(IpList).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
