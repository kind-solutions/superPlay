// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ResourceType.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Superplay.Protobuf.Messages {

  /// <summary>Holder for reflection information generated from ResourceType.proto</summary>
  public static partial class ResourceTypeReflection {

    #region Descriptor
    /// <summary>File descriptor for ResourceType.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResourceTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJSZXNvdXJjZVR5cGUucHJvdG8SCVN1cGVycGxheSpACgxSZXNvdXJjZVR5",
            "cGUSFwoTUkVTT1VSQ0VfVFlQRV9DT0lOUxAAEhcKE1JFU09VUkNFX1RZUEVf",
            "Uk9MTFMQAUIeqgIbU3VwZXJwbGF5LlByb3RvYnVmLk1lc3NhZ2VzYgZwcm90",
            "bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Superplay.Protobuf.Messages.ResourceType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum ResourceType {
    [pbr::OriginalName("RESOURCE_TYPE_COINS")] Coins = 0,
    [pbr::OriginalName("RESOURCE_TYPE_ROLLS")] Rolls = 1,
  }

  #endregion

}

#endregion Designer generated code