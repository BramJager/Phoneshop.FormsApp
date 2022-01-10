using Phoneshop.Domain.Entities;
using System.Collections.Generic;
using System.IO;

namespace Phoneshop.Business.Interfaces
{
    public interface IXmlService
    {
        List<Phone> Read(TextReader xml);
    }
}
