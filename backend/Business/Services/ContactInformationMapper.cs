using Business.Interfaces;

namespace Business.Services
{
    public class ContactInformationMapper
    {
        public void ReplaceContactInformationWith(IContactInformation newInformation, IContactInformation target)
        {
            target.FirstName = newInformation.FirstName;
            target.LastName = newInformation.LastName;
            target.PhoneNumber = newInformation.PhoneNumber;
            target.AlternativePhoneNumber = newInformation.AlternativePhoneNumber;
            target.Email = newInformation.Email;
            target.AlternativeEmail = newInformation.AlternativeEmail;
            target.DateOfBirth = newInformation.DateOfBirth;
            target.Address = newInformation.Address;
            target.Notes = newInformation.Notes;
        }
    }
}
