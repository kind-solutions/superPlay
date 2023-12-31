// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GiftEvent.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Superplay.Protobuf.Messages {

  /// <summary>Holder for reflection information generated from GiftEvent.proto</summary>
  public static partial class GiftEventReflection {

    #region Descriptor
    /// <summary>File descriptor for GiftEvent.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GiftEventReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9HaWZ0RXZlbnQucHJvdG8SCVN1cGVycGxheRoSUmVzb3VyY2VUeXBlLnBy",
            "b3RvGhNSZXNvdXJjZVZhbHVlLnByb3RvGgxQbGF5ZXIucHJvdG8ifgoJR2lm",
            "dEV2ZW50Eh8KBGZyb20YASABKAsyES5TdXBlcnBsYXkuUGxheWVyEiUKBHR5",
            "cGUYAiABKA4yFy5TdXBlcnBsYXkuUmVzb3VyY2VUeXBlEikKB2FtbW91bnQY",
            "AyABKAsyGC5TdXBlcnBsYXkuUmVzb3VyY2VWYWx1ZUIeqgIbU3VwZXJwbGF5",
            "LlByb3RvYnVmLk1lc3NhZ2VzYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Superplay.Protobuf.Messages.ResourceTypeReflection.Descriptor, global::Superplay.Protobuf.Messages.ResourceValueReflection.Descriptor, global::Superplay.Protobuf.Messages.PlayerReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Superplay.Protobuf.Messages.GiftEvent), global::Superplay.Protobuf.Messages.GiftEvent.Parser, new[]{ "From", "Type", "Ammount" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class GiftEvent : pb::IMessage<GiftEvent>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GiftEvent> _parser = new pb::MessageParser<GiftEvent>(() => new GiftEvent());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<GiftEvent> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Superplay.Protobuf.Messages.GiftEventReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GiftEvent() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GiftEvent(GiftEvent other) : this() {
      from_ = other.from_ != null ? other.from_.Clone() : null;
      type_ = other.type_;
      ammount_ = other.ammount_ != null ? other.ammount_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GiftEvent Clone() {
      return new GiftEvent(this);
    }

    /// <summary>Field number for the "from" field.</summary>
    public const int FromFieldNumber = 1;
    private global::Superplay.Protobuf.Messages.Player from_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Superplay.Protobuf.Messages.Player From {
      get { return from_; }
      set {
        from_ = value;
      }
    }

    /// <summary>Field number for the "type" field.</summary>
    public const int TypeFieldNumber = 2;
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
    public const int AmmountFieldNumber = 3;
    private global::Superplay.Protobuf.Messages.ResourceValue ammount_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Superplay.Protobuf.Messages.ResourceValue Ammount {
      get { return ammount_; }
      set {
        ammount_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as GiftEvent);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GiftEvent other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(From, other.From)) return false;
      if (Type != other.Type) return false;
      if (!object.Equals(Ammount, other.Ammount)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (from_ != null) hash ^= From.GetHashCode();
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) hash ^= Type.GetHashCode();
      if (ammount_ != null) hash ^= Ammount.GetHashCode();
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
      if (from_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(From);
      }
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Type);
      }
      if (ammount_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Ammount);
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
      if (from_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(From);
      }
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Type);
      }
      if (ammount_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Ammount);
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
      if (from_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(From);
      }
      if (Type != global::Superplay.Protobuf.Messages.ResourceType.Coins) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Type);
      }
      if (ammount_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Ammount);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GiftEvent other) {
      if (other == null) {
        return;
      }
      if (other.from_ != null) {
        if (from_ == null) {
          From = new global::Superplay.Protobuf.Messages.Player();
        }
        From.MergeFrom(other.From);
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
          case 10: {
            if (from_ == null) {
              From = new global::Superplay.Protobuf.Messages.Player();
            }
            input.ReadMessage(From);
            break;
          }
          case 16: {
            Type = (global::Superplay.Protobuf.Messages.ResourceType) input.ReadEnum();
            break;
          }
          case 26: {
            if (ammount_ == null) {
              Ammount = new global::Superplay.Protobuf.Messages.ResourceValue();
            }
            input.ReadMessage(Ammount);
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
          case 10: {
            if (from_ == null) {
              From = new global::Superplay.Protobuf.Messages.Player();
            }
            input.ReadMessage(From);
            break;
          }
          case 16: {
            Type = (global::Superplay.Protobuf.Messages.ResourceType) input.ReadEnum();
            break;
          }
          case 26: {
            if (ammount_ == null) {
              Ammount = new global::Superplay.Protobuf.Messages.ResourceValue();
            }
            input.ReadMessage(Ammount);
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
