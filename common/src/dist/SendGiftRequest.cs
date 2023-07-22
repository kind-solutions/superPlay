// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SendGiftRequest.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Superplay.Protobuf.Messages {

  /// <summary>Holder for reflection information generated from SendGiftRequest.proto</summary>
  public static partial class SendGiftRequestReflection {

    #region Descriptor
    /// <summary>File descriptor for SendGiftRequest.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SendGiftRequestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVTZW5kR2lmdFJlcXVlc3QucHJvdG8SCVN1cGVycGxheRoSUmVzb3VyY2VU",
            "eXBlLnByb3RvGhNSZXNvdXJjZVZhbHVlLnByb3RvInUKD1NlbmRHaWZ0UmVx",
            "dWVzdBIlCgR0eXBlGAEgASgOMhcuU3VwZXJwbGF5LlJlc291cmNlVHlwZRIp",
            "CgdhbW1vdW50GAIgASgLMhguU3VwZXJwbGF5LlJlc291cmNlVmFsdWUSEAoI",
            "ZnJpZW5kSWQYAyABKAlCHqoCG1N1cGVycGxheS5Qcm90b2J1Zi5NZXNzYWdl",
            "c2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Superplay.Protobuf.Messages.ResourceTypeReflection.Descriptor, global::Superplay.Protobuf.Messages.ResourceValueReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Superplay.Protobuf.Messages.SendGiftRequest), global::Superplay.Protobuf.Messages.SendGiftRequest.Parser, new[]{ "Type", "Ammount", "FriendId" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SendGiftRequest : pb::IMessage<SendGiftRequest>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<SendGiftRequest> _parser = new pb::MessageParser<SendGiftRequest>(() => new SendGiftRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<SendGiftRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Superplay.Protobuf.Messages.SendGiftRequestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SendGiftRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SendGiftRequest(SendGiftRequest other) : this() {
      type_ = other.type_;
      ammount_ = other.ammount_ != null ? other.ammount_.Clone() : null;
      friendId_ = other.friendId_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public SendGiftRequest Clone() {
      return new SendGiftRequest(this);
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 1;
    private global::Superplay.Protobuf.Messages.ResourceType type_ = global::Superplay.Protobuf.Messages.ResourceType.Coins;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Superplay.Protobuf.Messages.ResourceType Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "ammount" field.</summary>
    public const int AmmountFieldNumber = 2;
    private global::Superplay.Protobuf.Messages.ResourceValue ammount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Superplay.Protobuf.Messages.ResourceValue Ammount {
      get { return ammount_; }
      set {
        ammount_ = value;
      }
    }

    /// <summary>Field number for the "friendId" field.</summary>
    public const int FriendIdFieldNumber = 3;
    private string friendId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string FriendId {
      get { return friendId_; }
      set {
        friendId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as SendGiftRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(SendGiftRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Type != other.Type) return false;
      if (!object.Equals(Ammount, other.Ammount)) return false;
      if (FriendId != other.FriendId) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) hash ^= Type.GetHashCode();
      if (ammount_ != null) hash ^= Ammount.GetHashCode();
      if (FriendId.Length != 0) hash ^= FriendId.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (ammount_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Ammount);
      }
      if (FriendId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(FriendId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Type);
      }
      if (ammount_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Ammount);
      }
      if (FriendId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(FriendId);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (ammount_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Ammount);
      }
      if (FriendId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FriendId);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(SendGiftRequest other) {
      if (other == null) {
        return;
      }
      if (other.Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        Type = other.Type;
      }
      if (other.ammount_ != null) {
        if (ammount_ == null) {
          Ammount = new global::Superplay.Protobuf.Messages.ResourceValue();
        }
        Ammount.MergeFrom(other.Ammount);
      }
      if (other.FriendId.Length != 0) {
        FriendId = other.FriendId;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Type = (global::Superplay.Protobuf.Messages.ResourceType) input.ReadEnum();
            break;
          }
          case 18: {
            if (ammount_ == null) {
              Ammount = new global::Superplay.Protobuf.Messages.ResourceValue();
            }
            input.ReadMessage(Ammount);
            break;
          }
          case 26: {
            FriendId = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Type = (global::Superplay.Protobuf.Messages.ResourceType) input.ReadEnum();
            break;
          }
          case 18: {
            if (ammount_ == null) {
              Ammount = new global::Superplay.Protobuf.Messages.ResourceValue();
            }
            input.ReadMessage(Ammount);
            break;
          }
          case 26: {
            FriendId = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
