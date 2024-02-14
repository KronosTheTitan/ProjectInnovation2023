using Mono.CecilX;

namespace Mirror.Editor.Weaver
{
    // not static, because ILPostProcessor is multithreaded
    public interface Logger
    {
        void Warning(string message);
        void Warning(string message, MemberReference mr);
        void Error(string message);
        void Error(string message, MemberReference mr);
    }
}
