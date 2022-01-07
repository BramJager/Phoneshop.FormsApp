using Phoneshop.Domain.Entities;
using System.Collections.Generic;

namespace Phoneshop.Domain.Interfaces
{
    public interface IPhoneService
    {
        /// <summary>
        /// Gets a single <seealso cref="Phone"/> by the given identifier
        /// </summary>
        /// <param name="id">The unique identifier of the phone that should be returned</param>
        /// <returns>A <seealso cref="Phone"/> filled with properties</returns>
        Phone Get(int id);

        /// <summary>
        /// Gets all the phones from the (data)source
        /// </summary>
        /// <returns>A list of types <seealso cref="Phone"/></returns>
        IEnumerable<Phone> Get();

        /// <summary>
        /// Searching for phone, based on a string. Searches in the brand, type and description.
        /// </summary>
        /// <param name="query">The searchterm</param>
        /// <returns>A list of phones that matches the query</returns>
        IEnumerable<Phone> Search(string query);

        /// <summary>
        /// Creates a single phone and writes it to the datasource
        /// </summary>
        /// <param name="phone">A single <seealso cref="Phone"/>, without an identifier, the should be saved to the datasource.</param>
        void Create(Phone phone);

        /// <summary>
        /// Creates a set of phones and writes them to the datasource
        /// </summary>
        /// <param name="phones">A lost of <seealso cref="Phone"/>, without an identifier, the should be saved to the datasource.</param>
        void Create(List<Phone> phones);

        /// <summary>
        /// Deletes an existing phone from the data source.
        /// </summary>
        /// <param name="id">The unique identifier of the phone to be deleted.</param>
        void Delete(int id);
    }
}
