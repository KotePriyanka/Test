using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income II & DI")]
    class TC024_VerifyLoansInconsistencyDecreasedIncome
    {
        TC022_VerifyLoansInconsistencyDecreasedIncome _test = new TC022_VerifyLoansInconsistencyDecreasedIncome();

        [TearDown]
        public void Aftermethod()
        {
            _test.Aftermethod();
        }

        [TestCase(600, "Other", "Other", "android", TestName = "TC024_VerifyLoansInconsistencyDecreasedIncome_NL_SACC_600"), Category("NL"), Retry(2)]
        [TestCase(4950, "Other", "Other", "ios", TestName = "TC024_VerifyLoansInconsistencyDecreasedIncome_NL_MACC_4950")]
        public void TC024_VerifyingLoansInconsistencyDecreasedIncome_NL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_NL(loanamount, reason1, reason2, mobiledevice, true);
        }

    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income II & DI")]
    class TC024_VerifyLoansInconsistencyDecreasedIncome_RL
    {
        TC022_VerifyLoansInconsistencyDecreasedIncome _test = new TC022_VerifyLoansInconsistencyDecreasedIncome();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1000, "Other", "Other", "android", TestName = "TC024_VerifyLoansInconsistencyDecreasedIncome_RL_SACC_1000"), Category("RL"), Retry(2)]
        [TestCase(2250, "Other", "Other", "ios", TestName = "TC024_VerifyLoansInconsistencyDecreasedIncome_RL_MACC_2250")]
        public void TC024_VerifyingLoansInconsistencyDecreasedIncome_RL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_RL(loanamount, reason1, reason2, mobiledevice, true);
        }
    }
}
