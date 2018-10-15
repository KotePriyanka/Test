using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Member Area Login")]
    class TC178_Verify_Email_Changed_in_MemberArea : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC178_Verify_Email_Changed_in_MemberArea_android_RL"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC178_Verify_Email_Changed_in_MemberArea_ios_RL")]
        public void TC178_Verify_Email_Changed_in_MemberArea_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                // Login with existing user
                 _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Edit Profile to add success override (Cp:P)
                if (GetPlatform(_driver))
                {
                    // Mobile Site flow
                    //Click on More button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on your profile link
                    _homeDetails.ClickMemberAreaEditProfileLnk();

                    //Click on personal details link
                    _personalDetails.ClickPersonalDetails();

                    //Fetch existing email
                    string email = _personalDetails.FetchRLEmail();

                    //Fetch updated email
                    string updatedEmail = _personalDetails.splitEmail(email);

                    //Re enter updated email in email text box
                    _personalDetails.reEnterEmail(updatedEmail);

                    //Click on save button
                    _personalDetails.clickSaveButton();

                    //Click on More button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on logout
                    _loanSetUpDetails.Logout();

                    //Relogin with updated email
                    _homeDetails.ReLoginUser(updatedEmail, TestData.Password);

                    //verify useer home page
                    Assert.IsTrue(_homeDetails.VerifyUserHomePage(), "Requext Money Button");

                    //Click on More button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    //Desktop flow
                    //Click on Edit profile link
                    _homeDetails.ClickMemberAreaEditProfileLnk();

                    //Click on personal details link
                    _personalDetails.ClickPersonalDetails();

                    //Fetch existing email
                    string email = _personalDetails.FetchRLEmail();
                   
                    //Fetch updated email
                    string updatedEmail = _personalDetails.splitEmail(email);
                    
                    //Re enter updated email in email text box
                    _personalDetails.reEnterEmail(updatedEmail);

                    //Click on save button
                    _personalDetails.clickSaveButton();

                    //Click on logout
                    _loanSetUpDetails.Logout();
                    
                    //Relogin with updated email
                    _homeDetails.ReLoginUser(updatedEmail, TestData.Password);

                    //verify useer home page
                    Assert.IsTrue(_homeDetails.VerifyUserHomePage(), "User Home Displayed with altered Email");
                    
                    //Click on logout
                    _loanSetUpDetails.Logout();

                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}