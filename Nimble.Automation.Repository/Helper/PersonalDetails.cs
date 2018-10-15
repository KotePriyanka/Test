using Nimble.Automation.Accelerators;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;

namespace Nimble.Automation.Repository
{
    public class PersonalDetails : TestUtility
    {
        private IPersonalDetails _personaldetailsLoc;

        private ActionEngine _act = null;

        private IWebDriver _driver = null;

        public string EmailID { get; set; }

        public PersonalDetails(IWebDriver driver, string strUserType)
        {
            if (GetPlatform(driver))
                _personaldetailsLoc = (strUserType == "NL") ? (IPersonalDetails)new PersonalDetailsMobileNLLoc() : new PersonalDetailsMobileRLLoc();
            else
                _personaldetailsLoc = (strUserType == "NL") ? (IPersonalDetails)new PersonalDetailsDesktopNLLoc() : new PersonalDetailsDesktopRLLoc();

            _act = new ActionEngine(driver);
            _driver = driver;
        }

        /// <summary>
        /// Enters the street name text.
        /// </summary>
        public void EnterStreetNameTxt()
        {
            string streetName = _act.getValue(_personaldetailsLoc.StreetName, "StreetName");
            streetName += " Rmsrv:0.9999";
            _act.EnterText(_personaldetailsLoc.StreetName, streetName);
        }

        public void EnterStreetNameTxtRL(string a)
        {
            string StreetName = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Bsp:Y Rmsrv:0.9999";
            string temp = StreetName;
            // string StreetName = _act.getValue(_personaldetailsLoc.StreetName, "StreetName");
            if (a.Equals(StreetName))
            {
                StreetName = temp;
            }
            else
            {
                StreetName = a;
            }
            _act.waitForVisibilityOfElement(_personaldetailsLoc.StreetName, 30);
            _driver.FindElement(_personaldetailsLoc.StreetName).Clear();
            Thread.Sleep(2000);
            _act.EnterText(_personaldetailsLoc.StreetName, StreetName);
        }

        public void EnterFraudMobileNoTxtRL(string fraudmobileNo)
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Mobilephone, 30);
            Thread.Sleep(2000);
            _act.EnterText(_personaldetailsLoc.Mobilephone, fraudmobileNo);
        }

        public void ClickEditDetails()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.EditDetails, 30);
            _act.click(_personaldetailsLoc.EditDetails, "EditDetails");
        }

        public void ClickPersonalDetails()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.PersonalDetails, 30);
            _act.click(_personaldetailsLoc.PersonalDetails, "PersonalDetails");
        }

        public void ClickContactDetails()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.ContactDetails, 30);
            _act.click(_personaldetailsLoc.ContactDetails, "ContactDetails");
        }

        public string FetchRLEmail()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Email, 30);
            string RLEmail = _act.getValue(_personaldetailsLoc.Email, "Email");
            return RLEmail;
        }

        /// <summary>
        /// Enters the street name text.
        /// </summary>
        /// <param name="rmsrvCode">The RMSRV code.</param>
        public void EnterStreetNameTxt(string rmsrvCode)
        {
            string streetName = _act.getValue(_personaldetailsLoc.StreetName, "StreetName");
            streetName += rmsrvCode;
            _act.EnterText(_personaldetailsLoc.StreetName, streetName);
        }

        public string FetchStreetNumber()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Streetnumber, 30);
            string streetNumber = _act.getValue(_personaldetailsLoc.Streetnumber, "Street Number");
            return streetNumber;
        }

        /// <summary>
        /// Selects the employment status list.
        /// </summary>
        /// <param name="employmentStatus">The employment status.</param>
        public void SelectEmploymentStatusLst(string employmentStatus)
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.EmploymentStatus, 30);
            _act.selectByOptionText(_personaldetailsLoc.EmploymentStatus, employmentStatus, "EmploymentStatus");
        }

        /// <summary>
        /// Selects the Unemployment desc list.
        /// </summary>
        /// <param name="employmentStatus">The employment status.</param>
        public void SelectUnEmploymentDescLst(string Unempdesc)
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.UnemploymentDesc, 30);
            _act.selectByOptionText(_personaldetailsLoc.UnemploymentDesc, Unempdesc, "Unempdesc");
        }

        /// <summary>
        /// Clicks the personal use button.
        /// </summary>

        public void ClickYesForPersonalUseBtn()
        {
            //      Thread.Sleep(2000);
            _act.waitForVisibilityOfElement(_personaldetailsLoc.PersonalUseYes, 30);
            _act.click(_personaldetailsLoc.PersonalUseYes, "PersonalUseYes");
        }

        /// <summary>
        /// Clicks the personal use button.
        /// </summary>

        public void ClickNoForPersonalUseBtn()
        {
            //      Thread.Sleep(2000);
            _act.waitForVisibilityOfElement(_personaldetailsLoc.PersonalUseNo, 30);
            _act.click(_personaldetailsLoc.PersonalUseNo, "PersonalUseNo");
        }
        /// <summary>
        /// Clicks the no short term loan status button.
        /// </summary>
        public void ClickNoShortTermLoanStatusBtn()
        {
            Thread.Sleep(2000);
            _act.waitForVisibilityOfElement(_personaldetailsLoc.ShortTermLoanStatusNo, 20);
            _act.JSClick(_personaldetailsLoc.ShortTermLoanStatusNo, "ShortTermLoanStatusNo");
        }

        /// <summary>
        /// Clicks the yes short term loan status button.
        /// </summary>
        public void ClickYesShortTermLoanStatusBtn()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.ShortTermLoanStatusYes, 20);
            _act.click(_personaldetailsLoc.ShortTermLoanStatusYes, "ShortTermLoanStatusYes");
        }

        /// <summary>
        /// Checks the read privacy button.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        public void CheckReadPrivacyBtn(string userType)
        {
            if (userType == "New")
            {
                string classtextReadPrivacy = _driver.FindElement(_personaldetailsLoc.ReadPrivacychecked).GetAttribute("class");
                if (!classtextReadPrivacy.Contains("checked"))
                {
                    _act.JSClick(_personaldetailsLoc.ReadPrivacychecked, "ReadPrivacychecked");
                }
            }
            else
            {
                string classtextReadPrivacy = _driver.FindElement(_personaldetailsLoc.ReadPrivacychecked).GetAttribute("class");
                if (!classtextReadPrivacy.Contains("checked"))
                {
                    _act.JSClick(_personaldetailsLoc.ReadPrivacychecked, "ReadPrivacychecked");
                }
            }

        }

        /// <summary>
        /// Checks the read credit button.
        /// </summary>
        /// <param name="userType">Type of the user.</param>
        public void CheckReadCreditBtn(string userType)
        {
            if (userType == "New")
            {
                string classtexReadCredit = _driver.FindElement(_personaldetailsLoc.ReadCreditGuidechecked).GetAttribute("class");
                if (!classtexReadCredit.Contains("checked"))
                {
                    _act.JSClick(_personaldetailsLoc.ReadCreditGuidechecked, "ReadCreditGuidechecked");
                }
            }
            else
            {
                string classtexReadCredit = _driver.FindElement(_personaldetailsLoc.ReadCreditGuidechecked).GetAttribute("class");
                if (!classtexReadCredit.Contains("checked"))
                {
                    _act.JSClick(_personaldetailsLoc.ReadCreditGuidechecked, "ReadCreditGuidechecked");
                }
            }
        }

        /// <summary>
        /// Clicks the personaldetails continue button.
        /// </summary>
        public void ClickPersonaldetailsContinueBtn()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.personaldetailscontinuebutton, 30);
            _act.click(_personaldetailsLoc.personaldetailscontinuebutton, "personaldetailscontinuebutton");

        }

        /// <summary>
        /// Clicks the personaldetails continue button returner loaner mobile.
        /// </summary>
        public void ClickPersonaldetailsContinueBtnRLMobile()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.personaldetailscontinuebuttonRLMobile, 30);
            _act.click(_personaldetailsLoc.personaldetailscontinuebuttonRLMobile, "personaldetailscontinuebuttonRLMobile");

        }

        /// <summary>
        /// Clicks the personaldetails request button returner loaner desktop.
        /// </summary>
        public void ClickPersonaldetailsRequestBtnRLDesktop()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.personaldetailsRequestbuttonRLDesktop, 20);
            _act.click(_personaldetailsLoc.personaldetailsRequestbuttonRLDesktop, "personaldetailsRequestbuttonRLDesktop");
        }

        /// <summary>
        /// Clicks the checkout continue button.
        /// </summary>
        public void ClickCheckoutContinueBtn()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.checkoutContinueButton, 200);
            _act.click(_personaldetailsLoc.checkoutContinueButton, "checkoutContinueButton");
        }

        /// <summary>
        /// Clicks the automatic verification button.
        /// </summary>
        public void ClickAutomaticVerificationBtn()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.AutomaticVerificationButton, 120);
            _act.click(_personaldetailsLoc.AutomaticVerificationButton, "AutomaticVerificationButton");
        }

        /// <summary>
        /// Gets the DNQ text.
        /// </summary>
        /// <returns></returns>
        public string GetDNQTxt()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.DNQText, 30);
            return _act.getText(_personaldetailsLoc.DNQText, "DNQText");
        }

        /// <summary>
        /// Gets the DNQ message.
        /// </summary>
        /// <returns>string message</returns>
        public string GetDNQMessage()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.DNQMessage, 30);
            string val = _act.getText(_personaldetailsLoc.DNQMessage, "DNQMessage");
            val = Regex.Replace(val, "\r\n", " ");
            return val;
        }

        /// <summary>
        /// Gets the unsuccess message
        /// </summary>
        /// <returns>string message</returns>
        public string GetUnsuccessMessage()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.DNQText, 30);
            return _act.getText(_personaldetailsLoc.DNQText, "unsuccessmsg");
        }

        /// <summary>
        /// Clicks the member area.
        /// </summary>
        public void ClickMemberArea()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.MemberAreaButton, 30);
            _act.click(_personaldetailsLoc.MemberAreaButton, "MemberAreaButton");
        }

        /// <summary>
        /// Click Updates your profile button.
        /// </summary>
        public void UpdateYourProfile()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.UpdateYourProfile, 30);
            _act.click(_personaldetailsLoc.UpdateYourProfile, "UpdateYourProfile");
        }

        //Mobile
        /// <summary>
        /// Clicks the continue button after email.
        /// </summary>
        public void clickContinueButtonAfterEmail()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Next2Button, 30);
            _act.click(_personaldetailsLoc.Next2Button, "clickContinueButtonAfterEmail");
        }

        //Mobile
        /// <summary>
        /// Clicks the name of the continue button after street for mobile.
        /// </summary>
        public void clickContinueButtonAfterStreetName()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Next3Button, 30);
            _act.click(_personaldetailsLoc.Next3Button, "clickContinueButtonAfterStreetName");
        }

        public PersonalDetailsDataObj PopulatePersonalDetails()
        {
            PersonalDetailsDataObj _obj = new PersonalDetailsDataObj
            {
                FirstName = RandomString(5),
                Email = RandomEmail()
            };

            string Password = "password";
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Title, 30);
            _act.selectByOptionText(_personaldetailsLoc.Title, RandomTitle(), "Title");
            Thread.Sleep(2000); //required for title select
            _act.EnterText(_personaldetailsLoc.FirstName, _obj.FirstName);
            _act.EnterText(_personaldetailsLoc.MiddleName, RandomString(4));
            _act.EnterText(_personaldetailsLoc.LastName, RandomString(4));
            if (GetPlatform(_driver))
            {
                _act.selectByOptionText(_personaldetailsLoc.Dob_Day, RandomDay(), "Day");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Month, RandomMonth(), "Month");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Year, RandomYear(), "Year");
            }
            else
            {
                _act.EnterText(_personaldetailsLoc.DOB, GetRandomDOB());
            }
            _act.EnterText(_personaldetailsLoc.Email, _obj.Email);
            _act.EnterText(_personaldetailsLoc.Password, Password);
            _act.EnterText(_personaldetailsLoc.ConfirmPassword, Password);

            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Next2Button, "clickContinueButtonAfterEmail");
            }
            _act.EnterText(_personaldetailsLoc.Homephone, "0" + RandomNumber(9));
            _act.EnterText(_personaldetailsLoc.Mobilephone, "04" + RandomNumber(8));
            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Address, "Address");
                _act.waitForVisibilityOfElement(_personaldetailsLoc.AddressSearch, 30);
                _act.EnterText(_personaldetailsLoc.AddressSearch, "TestAddress#");
                _act.waitForVisibilityOfElement(_personaldetailsLoc.AddressAutoFillBtn, 60);
                if (_act.isElementPresent(_personaldetailsLoc.AddressAutoFillBtn))
                {
                    _act.click(_personaldetailsLoc.AddressAutoFillBtn, "AddressAutoFillBtn");
                }
            }
            else
            {
                _act.EnterText(_personaldetailsLoc.Address, "TestAddress#");
                _act.waitForVisibilityOfElement(_personaldetailsLoc.AddressAutoFillBtn, 60);
                IWebElement addressAutofill = _driver.FindElement(_personaldetailsLoc.AddressAutoFillBtn);
                if (_act.isElementDisplayed(addressAutofill))
                {
                    _act.JSClick(_personaldetailsLoc.AddressAutoFillBtn, "AddressAutoFillBtn");
                }
            }
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Unitnumber, 60);
            Thread.Sleep(100);
            _act.EnterText(_personaldetailsLoc.Unitnumber, RandomNumber(3));
            Thread.Sleep(1000);
            _act.Sync(_personaldetailsLoc.Streetnumber);
            _act.EnterText(_personaldetailsLoc.Streetnumber, RandomNumber(3));
            Thread.Sleep(100);
            _act.EnterText(_personaldetailsLoc.StreetName, "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:0.9999"); //At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Bsp:Y Rmsrv:0.9999
            _act.EnterText(_personaldetailsLoc.Streettype, RandomStreeType());
            int index = Convert.ToInt32(RandomNumber(2));
            _act.EnterText(_personaldetailsLoc.Suburbtype, RandomSubrubPostCode(index, 0));
            _act.EnterText(_personaldetailsLoc.Postcode, RandomSubrubPostCode(index, 1));
            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Next3Button, "clickContinueButtonAfterStreetName");
            }
            _act.waitForVisibilityOfElement(_personaldetailsLoc.EmploymentStatus, 30);
            _act.selectByOptionText(_personaldetailsLoc.EmploymentStatus, "Full Time", "EmploymentStatus");
            _act.click(_personaldetailsLoc.ShortTermLoanStatusNo, "ShortTermLoanStatusNo");
            CheckReadPrivacyBtn("New");
            CheckReadCreditBtn("New");
            _act.click(_personaldetailsLoc.personaldetailscontinuebutton, "personaldetailscontinuebutton");


            TestUtility testut = new TestUtility();
            string strbuilder = DateTime.Now.ToString("MM-dd-yy HH:mm") + " " + TestContext.CurrentContext.Test.Name + "*" + _obj.Email + Environment.NewLine;
            EmailID = _obj.Email;
            Console.WriteLine(_obj.Email);
            testut.WriteToFile(strbuilder);

            return _obj;

        }

        public async void VerifyFraudEmail(PersonalDetailsDataObj _perData)
        {
            //check if object is null if not assign value
            PersonalDetailsDataObj _personalData = VerifyObj(_perData);

            _act.waitForVisibilityOfElement(_personaldetailsLoc.Title, 60);
            _act.selectByOptionText(_personaldetailsLoc.Title, _personalData.Title, "Title");
            Thread.Sleep(2000); // Required for Title select
            _act.EnterText(_personaldetailsLoc.FirstName, _personalData.FirstName);
            _act.EnterText(_personaldetailsLoc.MiddleName, _personalData.MiddleName);
            _act.EnterText(_personaldetailsLoc.LastName, _personalData.LastName);
            if (GetPlatform(_driver))
            {
                _act.selectByOptionText(_personaldetailsLoc.Dob_Day, _personalData.DOB_Day, "Day");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Month, _personalData.DOB_Month, "Month");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Year, _personalData.DOB_Year, "Year");
            }
            else
            {
                _act.EnterText(_personaldetailsLoc.DOB, _personalData.DOB);
            }
            _act.EnterText(_personaldetailsLoc.Email, _personalData.Email);

            string email = _driver.FindElement(_personaldetailsLoc.Email).GetAttribute("value");
            Console.WriteLine(email);
        }

        /// <summary>
        /// Verify Email Existing.
        /// </summary>
        public bool VerifyEmailExisting(string errormessage)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_personaldetailsLoc.ExistingEmailError, 60);
            string emailexisitingmsg = _act.getText(_personaldetailsLoc.ExistingEmailError, "ExistingEmailError");

            if (emailexisitingmsg.Contains(errormessage))
                flag = true;
            else
                flag = false;

            return flag;
        }

        public async void PopulatePersonalDetails(PersonalDetailsDataObj _perData)
        {
            //check if object is null if not assign value
            PersonalDetailsDataObj _personalData = VerifyObj(_perData);

            _act.waitForVisibilityOfElement(_personaldetailsLoc.Title, 60);
            _act.selectByOptionText(_personaldetailsLoc.Title, _personalData.Title, "Title");
            Thread.Sleep(2000); // Required for Title select
            _act.EnterText(_personaldetailsLoc.FirstName, _personalData.FirstName);
            _act.EnterText(_personaldetailsLoc.MiddleName, _personalData.MiddleName);
            _act.EnterText(_personaldetailsLoc.LastName, _personalData.LastName);
            if (GetPlatform(_driver))
            {
                _act.selectByOptionText(_personaldetailsLoc.Dob_Day, _personalData.DOB_Day, "Day");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Month, _personalData.DOB_Month, "Month");
                _act.selectByOptionText(_personaldetailsLoc.Dob_Year, _personalData.DOB_Year, "Year");
            }
            else
            {
                _act.EnterText(_personaldetailsLoc.DOB, _personalData.DOB);
            }
            _act.EnterText(_personaldetailsLoc.Email, _personalData.Email);
            _act.EnterText(_personaldetailsLoc.Password, _personalData.Password);
            _act.EnterText(_personaldetailsLoc.ConfirmPassword, _personalData.ConfirmPassword);
            // string email = _driver.FindElement(_personaldetailsLoc.Email).GetAttribute("value");
            Console.WriteLine(_personalData.Email);
            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Next2Button, "clickContinueButtonAfterEmail");
            }
            _act.EnterText(_personaldetailsLoc.Homephone, _personalData.HomePhone);
            _act.EnterText(_personaldetailsLoc.Mobilephone, _personalData.MobilePhone);
            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Address, "Address");
                _act.waitForVisibilityOfElement(_personaldetailsLoc.AddressSearch, 60);
                _act.EnterText(_personaldetailsLoc.AddressSearch, "TestAddress#");
                IWebElement addressAutofill = _driver.FindElement(_personaldetailsLoc.AddressAutoFillBtn);
                // if(_act.isElementDisplayed(AddressAutofill))
                if (_act.isElementPresent(_personaldetailsLoc.AddressAutoFillBtn))
                {
                    _act.click(_personaldetailsLoc.AddressAutoFillBtn, "AddressAutoFillBtn");
                }
            }
            else
            {
                _act.EnterText(_personaldetailsLoc.Address, "TestAddress#");
                _act.waitForVisibilityOfElement(_personaldetailsLoc.AddressAutoFillBtn, 120);
                IWebElement addressAutofill = _driver.FindElement(_personaldetailsLoc.AddressAutoFillBtn);
                if (_act.isElementDisplayed(addressAutofill))
                {
                    _act.click(_personaldetailsLoc.AddressAutoFillBtn, "AddressAutoFillBtn");
                }
            }
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Unitnumber, 60);
            _act.EnterText(_personaldetailsLoc.Unitnumber, _personalData.UnitNumber);
            Thread.Sleep(1000);
            _act.Sync(_personaldetailsLoc.Streetnumber);
            _act.EnterText(_personaldetailsLoc.Streetnumber, _personalData.StreetNumber);
            Thread.Sleep(1000);
            _act.EnterText(_personaldetailsLoc.StreetName, _personalData.StreetName + _personalData.Rmsrvcode);
            _act.EnterText(_personaldetailsLoc.Streettype, _personalData.StreetType);
            _act.EnterText(_personaldetailsLoc.Suburbtype, _personalData.Suburb);
            _act.EnterText(_personaldetailsLoc.Postcode, _personalData.PostCode);

            if (GetPlatform(_driver))
            {
                _act.click(_personaldetailsLoc.Next3Button, "clickContinueButtonAfterStreetName");
            }
            _act.selectByOptionText(_personaldetailsLoc.EmploymentStatus, _personalData.EmploymentStatus, "EmploymentStatus");

            if (_personalData.EmploymentStatus == "Unemployed")
            {
                _act.selectByOptionText(_personaldetailsLoc.UnemploymentDesc, _personalData.UnemploymentDesc, "UnEmploymentDesc");
            }

            if (_personalData.Have2SACCLoan == "Yes")
            {
                _act.click(_personaldetailsLoc.ShortTermLoanStatusYes, "ShortTermLoanStatusYes");
            }
            else
            {
                _act.click(_personaldetailsLoc.ShortTermLoanStatusNo, "ShortTermLoanStatusNo");
            }
            CheckReadPrivacyBtn(_personalData.UserType);
            CheckReadCreditBtn(_personalData.UserType);

            _act.click(_personaldetailsLoc.personaldetailscontinuebutton, "personaldetailscontinuebutton");

            TestUtility _testut = new TestUtility();
            string strbuilder = DateTime.Now.ToString("MM-dd-yy HH:mm") + " " + TestContext.CurrentContext.Test.Name + "*" + _personalData.Email + Environment.NewLine;
            EmailID = _personalData.Email;
            await _testut.WriteToFile(strbuilder);
        }

        public PersonalDetailsDataObj PersonalDetailsFunction()
        {
            PersonalDetailsDataObj _obj = PopulatePersonalDetails();
            // Click on checks out Continue Button
            ClickCheckoutContinueBtn();

            return _obj;
        }
        public PersonalDetailsDataObj PersonalDetailsFunction1()
        {
            PersonalDetailsDataObj _obj = PopulatePersonalDetails();
            // Click on checks out Continue Button
            //ClickCheckoutContinueBtn();
            return _obj;
        }

        public string FetchEmailfromRLuser_RL()
        {
            //Click on personal details Button
            ClickPersonalDetails();

            // Get Email from textbox
            string emailfromRl = FetchRLEmail();
            return emailfromRl;

        }

        public string PersonalDetailsFunction_RL(string empStatus, string ReturnerLoaner, string streetname)
        {
            //Click on Edit My Details Button
            ClickEditDetails();

            string emailstring = FetchEmailfromRLuser_RL();

            //Click on ContactDetails
            ClickContactDetails();

            //Change streetname
            EnterStreetNameTxtRL(streetname);

            // select Employement Status
            SelectEmploymentStatusLst(empStatus);

            // select short term loans value as NO
            ClickNoShortTermLoanStatusBtn();

            // Check Read Privacy and Electronic Authorisation
            CheckReadPrivacyBtn(ReturnerLoaner);

            // Check Read Credit Guide
            CheckReadCreditBtn(ReturnerLoaner);

            if (GetPlatform(_driver))
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsContinueBtnRLMobile();
            }
            else
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsRequestBtnRLDesktop();

                // Click on checks out Continue Button
                ClickAutomaticVerificationBtn();
            }

            return emailstring;
        }

        public string PersonalDetailsFunction_Skipbanklogin(string empStatus, string ReturnerLoaner, string streetname)
        {
            //Click on Edit My Details Button
            ClickEditDetails();

            string emailstring = FetchEmailfromRLuser_RL();

            //Click on ContactDetails
            ClickContactDetails();

            //Change streetname
            EnterStreetNameTxtRL(streetname);

            // select Employement Status
            SelectEmploymentStatusLst(empStatus);

            // select short term loans value as NO
            ClickNoShortTermLoanStatusBtn();

            // Check Read Privacy and Electronic Authorisation
            CheckReadPrivacyBtn(ReturnerLoaner);

            // Check Read Credit Guide
            CheckReadCreditBtn(ReturnerLoaner);

            if (GetPlatform(_driver))
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsContinueBtnRLMobile();
            }
            else
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsRequestBtnRLDesktop();

                // Click on checks out Continue Button
                // ClickAutomaticVerificationBtn();
            }

            return emailstring;
        }

        public void PersonalDetailsFraudBSB_RL(string empStatus, string ReturnerLoaner, string streetname)
        {
            //Click on ContactDetails
            ClickContactDetails();

            //Change streetname
            EnterStreetNameTxtRL(streetname);

            // select Employement Status
            SelectEmploymentStatusLst(empStatus);

            // select short term loans value as NO
            ClickNoShortTermLoanStatusBtn();

            // Check Read Privacy and Electronic Authorisation
            CheckReadPrivacyBtn(ReturnerLoaner);

            // Check Read Credit Guide
            CheckReadCreditBtn(ReturnerLoaner);

            if (GetPlatform(_driver))
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsContinueBtnRLMobile();
            }
            else
            {
                // Click on Personal Details Continue Button
                ClickPersonaldetailsRequestBtnRLDesktop();

                // Click on checks out Continue Button
                //ClickAutomaticVerificationBtn();
            }
        }

        public PersonalDetailsDataObj VerifyObj(PersonalDetailsDataObj _obj)
        {
            PersonalDetailsDataObj _object = new PersonalDetailsDataObj
            {
                Title = _obj.Title ?? RandomTitle(),
                FirstName = _obj.FirstName ?? RandomString(5),
                MiddleName = _obj.MiddleName ?? RandomString(4),
                LastName = _obj.LastName ?? RandomString(4),
                DOB = _obj.DOB ?? GetRandomDOB(),
                DOB_Day = _obj.DOB_Day ?? RandomDay(),
                DOB_Month = _obj.DOB_Month ?? RandomMonth(),
                DOB_Year = _obj.DOB_Year ?? RandomYear(),
                Email = _obj.Email ?? RandomEmail(),
                Password = _obj.Password ?? "password",
                ConfirmPassword = _obj.ConfirmPassword ?? "password",
                HomePhone = _obj.HomePhone ?? "0" + RandomNumber(9),
                MobilePhone = _obj.MobilePhone ?? "04" + RandomNumber(8),
                Address = _obj.Address ?? "TestAddress#@",
                UnitNumber = _obj.UnitNumber ?? RandomNumber(4),
                StreetNumber = _obj.StreetNumber ?? RandomNumber(3),
                Rmsrvcode = _obj.Rmsrvcode ?? " Rmsrv:0.9999",
                StreetName = _obj.StreetName ?? "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y",
                StreetType = _obj.StreetType ?? RandomStreeType()
            };
            int index = Convert.ToInt32(RandomNumber(2));
            _object.Suburb = _obj.Suburb ?? RandomSubrubPostCode(index, 0);
            _object.PostCode = _obj.PostCode ?? RandomSubrubPostCode(index, 1);
            _object.EmploymentStatus = _obj.EmploymentStatus ?? "Full Time";
            _object.UnemploymentDesc = _obj.UnemploymentDesc ?? "Student";
            _object.Have2SACCLoan = _obj.Have2SACCLoan ?? "No";
            _object.UserType = _obj.UserType ?? "New";

            return _object;
        }

        public void reEnterEmail(string email)
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Email, 30);
            _act.EnterText(_personaldetailsLoc.Email, email);
        }

        public void clickSaveButton()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.SaveButton, 30);
            _act.JSClick(_personaldetailsLoc.SaveButton, "Save");
            Thread.Sleep(1000);
        }

        public string splitEmail(string email)
        {
            MailAddress addr = new MailAddress(email);
            string username = addr.User;
            string domain = addr.Host;
            string updatedUsername = username + "1";
            string updatedEmail = updatedUsername + "" + "@" + domain;
            Console.WriteLine("Updated Email is: " + updatedEmail);
            return updatedEmail;
        }

        public void enterUnitNumber()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.Unitnumber, 60);
            _act.EnterText(_personaldetailsLoc.Unitnumber, "12345");
        }

        public void enterStreetName(string streetNumber)
        {
            _act.waitForInVisibilityOfElement(_personaldetailsLoc.Streetnumber, "Streetname");
            _act.EnterText(_personaldetailsLoc.Streetnumber, streetNumber);
        }

        public void clickContactSaveButtonMob()
        {
            _act.waitForVisibilityOfElement(_personaldetailsLoc.ContactPageSaveButton, 30);
            _act.JSClick(_personaldetailsLoc.ContactPageSaveButton, "Save");
            Thread.Sleep(1000);
        }


    }

    /// <summary>
    /// Personal Details object with all the properites
    /// </summary>
    public class PersonalDetailsDataObj
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string DOB_Day { get; set; }
        public string DOB_Month { get; set; }
        public string DOB_Year { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public string UnitNumber { get; set; }
        public string StreetNumber { get; set; }
        public string Rmsrvcode { get; set; }
        public string StreetType { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string StreetName { get; set; }
        public string EmploymentStatus { get; set; }
        public string UnemploymentDesc { get; set; }
        public string Have2SACCLoan { get; set; }
        public string UserType { get; set; }

    }

}