using System.ComponentModel;

namespace Script.Enum
{
    public enum ContextType
    {
        None,
        [Description("123456")]
        FormData, //multipart/form-data; boundary=<calculated when request is sent>
        UrlEncoded,//application/x-www-form-urlencoded
        Json,//application/json
    }
}