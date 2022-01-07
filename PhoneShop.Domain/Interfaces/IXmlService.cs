using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.IO;

namespace Phoneshop.Domain.Interfaces
{
    public interface IXmlService
    {
        List<Phone> Read(TextReader xml);
    }
}
