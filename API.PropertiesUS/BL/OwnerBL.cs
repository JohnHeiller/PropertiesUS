using API.PropertiesUS.DAL.Dominio;
using API.PropertiesUS.DAL.Repo;
using API.PropertiesUS.DTO;
using System;
using System.Collections.Generic;

namespace API.PropertiesUS.BL
{
    /// <summary>
    /// Class for business validations of the entity Owner
    /// </summary>
    public class OwnerBL : IOwnerBL
    {
        private readonly IBaseRepo<Owners> _ownersRepo;
        private readonly UtilBL _utilBL;
        /// <summary>
        /// Class constructor
        /// </summary>
        public OwnerBL(string connectionString = null)
        {
            _ownersRepo = new BaseRepo<Owners>(connectionString);
            _utilBL = new UtilBL();
        }

        /// <summary>
        /// Method to validate the correct creation of an Owner record
        /// </summary>
        /// <param name="owner">OwnerDTO with Owner data to register</param>
        /// <returns>New record identifier</returns>
        public long CreateOwner(OwnerDTO owner)
        {
            try
            {
                bool isValidDate = false;
                DateTime newBirthday = DateTime.Now;
                if (string.IsNullOrWhiteSpace(owner.Name)
                    && string.IsNullOrWhiteSpace(owner.Address))
                {
                    throw new Exception("Error in owner registration data.");
                }
                if (!string.IsNullOrWhiteSpace(owner.Birthday))
                {
                    isValidDate = DateTime.TryParse(owner.Birthday, out newBirthday);
                }
                Owners newOwner = new Owners()
                {
                    Address = owner.Address,
                    Name = owner.Name,
                    Photo = !string.IsNullOrWhiteSpace(owner.PhotoBase64) ? owner.PhotoBase64 : ((owner.Photo != null) ? _utilBL.GetBase64(owner.Photo) : string.Empty),
                    Birthday = isValidDate ? (DateTime?)newBirthday : null
                };
                newOwner = _ownersRepo.Add(newOwner);
                return newOwner.IdOwner;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// Method to get all existing records of Owners
        /// </summary>
        /// <returns>List of queried records of type Owners</returns>
        public List<Owners> GetAll()
        {
            try
            {
                var owners = _ownersRepo.GetAll();
                return owners;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
}
