using Ostis.Sctp.Arguments;
namespace Ostis.Sctp.Tools
{
   public  interface IElement
    {
       ScAddress ScAddress { get; }
       ElementType Type { get; }
    }
}
