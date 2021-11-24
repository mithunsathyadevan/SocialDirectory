using Moq;
using SocailDirectoryServices.Interest;
using SocialDirectoryContracts.Interest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SocialDirectoryUnitTest.ServicesUnitTests
{
   public class InterestServiceUnitTest
    {
        Mock<InterestContract> _interestService;
        List<SocailDirectoryModels.Models.Interest> returnModel;
        public InterestServiceUnitTest()
        {
            _interestService = new Mock<InterestContract>();
            SocailDirectoryModels.Models.Interest model = new SocailDirectoryModels.Models.Interest
            {
                Id = 1,
                InterestName = "Interest1"
            };
            returnModel = new List<SocailDirectoryModels.Models.Interest>();
            returnModel.Add(model);
        }
       
        [Fact]
        public async Task TestGetAllInterest()
        {            
            _interestService.Setup(x => x.GetAllInterest()).ReturnsAsync(returnModel);
            var output = await _interestService.Object.GetAllInterest();
            Assert.IsType < List<SocailDirectoryModels.Models.Interest>>(output);
        }
        [Fact]
        public async Task TestGetMasterInterestBySearchParameter()
        {           
            _interestService.Setup(x => x.GetMasterInterests(null)).ReturnsAsync(returnModel);
            var output = await _interestService.Object.GetMasterInterests(null);
            Assert.IsType<List<SocailDirectoryModels.Models.Interest>>(output);
        }
    }
}
