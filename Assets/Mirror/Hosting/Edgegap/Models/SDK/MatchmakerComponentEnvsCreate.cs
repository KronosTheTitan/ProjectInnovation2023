using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Mirror.Hosting.Edgegap.Models.SDK {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class MatchmakerComponentEnvsCreate {
    /// <summary>
    /// Name of the ENV variable.
    /// </summary>
    /// <value>Name of the ENV variable.</value>
    [DataMember(Name="key", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "key")]
    public string Key { get; set; }

    /// <summary>
    /// Value of the ENV variable.
    /// </summary>
    /// <value>Value of the ENV variable.</value>
    [DataMember(Name="value", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()  {
      StringBuilder sb = new StringBuilder();
      sb.Append("class MatchmakerComponentEnvsCreate {\n");
      sb.Append("  Key: ").Append(Key).Append("\n");
      sb.Append("  Value: ").Append(Value).Append("\n");
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
