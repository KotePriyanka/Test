using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC018_VerifyInconsistencyIncome
    {
        TC017_VerifyInconsistencyIncome _test = new TC017_VerifyInconsistencyIncome();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        //Fix 4950
        [TestCase(600, "No", "Yes", "android", TestName = "TC018_VerifyInconsistencyIncome_NL_SACC_600"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(4950, "No", "Yes", "ios", TestName = "TC018_VerifyInconsistencyIncome_NL_MACC_4950")]
        public void TC018_VerifyingInconsistencyIncome_NL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_NL(loanamount, reason1, reason2, mobiledevice);
        }
    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC018_VerifyInconsistencyIncome_RL
    {
        TC017_VerifyInconsistencyIncome _test = new TC017_VerifyInconsistencyIncome();
        //fix 2250
       
        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1000, "No", "Yes", "android", TestName = "TC018_VerifyInconsistencyIncome_RL_SACC_1000"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2250, "No", "Yes", "ios", TestName = "TC018_VerifyInconsistencyIncome_RL_MACC_2250")]
        public void TC018_VerifyingInconsistencyIncome_RL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_RL(loanamount, reason1, reason2, mobiledevice);
        }
    }
}
