using Phoneshop.Business.Interfaces;
using Phoneshop.Domain.Entities;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class XmlService : IXmlService
    {
        private readonly IBrandService brandService;

        public XmlService(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public List<Phone> Read(TextReader xml)
        {
            if (xml is null)
                throw new ArgumentNullException(nameof(xml));

            var result = new List<Phone>();

            using (XmlReader reader = XmlReader.Create(xml))
            {
                Phone p = null;

                while (reader.Read())
                {
                    if (!reader.IsStartElement() && reader.Name == "Phone")
                        result.Add(p);

                    if (reader.IsStartElement() && reader.Name == "Phone")
                        p = new Phone();

                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Brand":
                                if (reader.Read())
                                {
                                    p.Brand = brandService.GetOrCreate(reader.Value);
                                }

                                break;
                            case "Type":
                                if (reader.Read())
                                    p.Type = reader.Value;
                                break;
                            case "Price":
                                if (reader.Read())
                                    p.Price = Convert.ToDouble(reader.Value);
                                break;
                            case "Description":
                                if (reader.Read())
                                    p.Description = reader.Value;
                                break;
                            case "Stock":
                                if (reader.Read())
                                    p.Stock = Convert.ToInt32(reader.Value);
                                break;
                        }
                    }

                }
            }

            return result;
        }
    }
}
