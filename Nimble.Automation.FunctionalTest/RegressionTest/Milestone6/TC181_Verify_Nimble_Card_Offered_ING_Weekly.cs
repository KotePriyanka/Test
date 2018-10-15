using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Nimble Card Offered INZ Weekly")]
    class TC181_Verify_Nimble_Card_Offered_ING_Weekly : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC181_Verify_Nimble_Card_Offered_INZ_Weekly_android_RL"), Category("RL"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC181_Verify_Nimble_Card_Offered_INZ_Weekly_ios_RL")]
        public void TC181_Verify_Nimble_Card_Offered_INZ_Weekly_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NimbleCardEligibility, TestData.Feature.INGactiveweeklycardoffered);

                if (GetPlatform(_driver))
                {
                    // Mobile Site flow
                    //Check availability of Bpay link
                    Assert.IsFalse(_loanSetUpDetails.verifyBpayPageLink(), "Bpay page link");

                    //Click on card
                    _loanSetUpDetails.clickCardMob();

                    //Order card
                    _loanSetUpDetails.orderCard();

                    //Activate card
                    _loanSetUpDetails.ActivateCard();

                    //Click on card
                    _loanSetUpDetails.clickCardMob();

                    //Click on your card link
                    _loanSetUpDetails.clickYourCardLink();

                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Verify your card link
                    //Assert.IsTrue(_loanSetUpDetails.verifyCardPage(), "Status of the card");
                    //Assert.AreEqual(_loanSetUpDetails.verifyYourcardPage(), "Status:");

                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Click on transaction history link
                    _loanSetUpDetails.clickTransactionHistoryLink();

                    //Verify transaction history link page
                    Assert.AreEqual(_loanSetUpDetails.verifyTransactionHistroyMessage(), "Transaction History");

                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Click Pay anyone link
                    _loanSetUpDetails.clickPayanyoneLink();

                    //Verify payanyone page
                    Assert.AreEqual(_loanSetUpDetails.verifyPayanyoneMessage(), "Pay Anyone");

                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Click BPAY Link
                    _loanSetUpDetails.clickBpayLink();

                    //verify Bpay page
                    Assert.AreEqual(_loanSetUpDetails.verifyBpayPage(), "Make Payment");

                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Click on more button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    //Desktop flow
                    //Check availability of Bpay link
                    Assert.IsFalse(_loanSetUpDetails.verifyBpayPageLink(), "Bpay page link");

                    //Order card
                    _loanSetUpDetails.orderCard();

                    //Activate card
                    _loanSetUpDetails.ActivateCard();

                    //Click on your card link
                    _loanSetUpDetails.clickYourCardLink();

                    //Verify your card link
                    Assert.AreEqual(_loanSetUpDetails.verifyYourcardPage(), "Card Details");

                    //Click BPAY Link
                    _loanSetUpDetails.clickBpayLink();

                    //verify Bpay page
                    Assert.AreEqual(_loanSetUpDetails.verifyBpayPage(), "BPAY® - Make a Payment");

                    //Click Pay anyone link
                    _loanSetUpDetails.clickPayanyoneLink();

                    //Verify payanyone page
                    Assert.IsTrue(_loanSetUpDetails.verifyPayanyonePage(), "BSB Textbox");

                    //Click on transaction history link
                    _loanSetUpDetails.clickTransactionHistoryLink();

                    //Verify transaction history link page
                    Assert.IsTrue(_loanSetUpDetails.verifyTransactionHistoryPage(), "Filter transaction button");

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