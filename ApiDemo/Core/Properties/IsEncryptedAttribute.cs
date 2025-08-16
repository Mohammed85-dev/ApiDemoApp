using ApiDemo.Core.Encryption;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace ApiDemo.Core.Properties;

[PSerializable]
public class IsEncryptedAttribute : LocationInterceptionAspect {
    public override void OnSetValue(LocationInterceptionArgs args) {
        args.Value = SHA_256.ComputeSha256Hash((string)args.Value);

        args.ProceedSetValue();
    }
}