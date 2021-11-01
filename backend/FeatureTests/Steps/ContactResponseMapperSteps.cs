using Application.Dto.Contact;
using Application.Services;
using Business.Services;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace FeatureTests.Steps
{
    [Binding]
    public class ContactResponseMapperSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly MapperService _mapperService;

        public ContactResponseMapperSteps(ScenarioContext cenarioContext)
        {
            _scenarioContext = cenarioContext;
            _mapperService = new MapperService(new ContactInformationMapper());
        }

        [Given(@"the user has a contact that isn't shared with anyone")]
        public void GivenTheUserHasAContactThatIsnTSharedWithAnyone()
        {
            _scenarioContext["Contact"] = new Contact();
        }

        [Given(@"the user has a contact that is shared, but unaccepted")]
        public void GivenTheUserHasAContactThatIsSharedButUnaccepted()
        {
            _scenarioContext["Contact"] = new Contact
            {
                UnacceptedShares = new List<UnacceptedShare>()
                {
                    new UnacceptedShare()
                    {
                        User = new User()
                    }
                },
                ContactUsers = new List<ContactUser>()
            };
        }

        [Given(@"the user has a contact that is shared and accepted")]
        public void GivenTheUserHasAContactThatIsSharedAndAccepted()
        {
            _scenarioContext["Contact"] = new Contact
            {
                UnacceptedShares = new List<UnacceptedShare>(),
                ContactUsers = new List<ContactUser>
                {
                    new ContactUser()
                    {
                        User = new User()
                    }
                }
            };
        }

        [When(@"this contact is mapped to a ContactResponse")]
        public void WhenThisContactIsMappedToAContactResponse()
        {
            _scenarioContext["ContactResponse"] =
                _mapperService.SharedOrOther((Contact)_scenarioContext["Contact"]);
        }
        
        [Then(@"the retrieved contact should be of type (.*)")]
        public void ThenTheRetrievedContactShouldBeOfTypeOTHER(string type)
        {
            var response = (ContactResponse)_scenarioContext["ContactResponse"];
            Assert.AreEqual(response.Type, type);
        }
    }
}
