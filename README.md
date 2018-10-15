# nimble-ui-automation

The following instructions details to execute Automation test suit

**Prerequisites"**

- Visual Studio

- Chrome Browser

**Steps to Execute Test Cases in Local Chrome**

- Clone or Download Source Code

- Open Solution in Visual Studio

- Select "SmokeTestSuit" branch

- Select Configuration - DebugLocaChrome (to execute scripts on local chrome browser)

- Copy chromedriver.exe

 from : ..\nimble-ui-automation\packages\Selenium.Chrome.WebDriver.2.25\driver\

 to : ..\nimble-ui-automation\Nimble.Automation.FunctionalTest\bin\DebugLocalChrome\

- Build nimble-ui-automation solution

- Open Test Explore from Menu --> Test --> Windows --> Text Explorer

- Select Required test

- Click on Start button

***

**Steps to Execute Test Cases in Local Mobile**

Prerequisite : [Appium](http://appium.io/slate/en/v1.1.0/?ruby#i-don-39-t-get-it-yet)

- Clone or Download Source Code

- Select Configuration - DebugLocaMobile (to execute scripts on local chrome browser)

- Update Device information App.cong

- Build nimble-ui-automation solution

- Start Appium Engine

- Check device is listed 

- Open Test Explore from Menu --> Test --> Windows --> Text Explorer

- Select Required test

***

**Steps to Execute Test Cases in Browser Stack Chrome**

- Clone or Download Source Code

- Select Configuration - DebugBSChrome (to execute scripts on local chrome browser)

- Build nimble-ui-automation solution

- Open Test Explore from Menu --> Test --> Windows --> Text Explorer

- Select Required test

- Login to Browser Stack to view the execution

**BrowserStack Credientials:**

**URL:** https://www.browserstack.com/automate

**UserName:** Spottlacher

**Password:** Cigniti@123

***

**Steps to Execute Test Cases in Browser Stack Mobile**

- Clone or Download Source Code

- Select Configuration - DebugBSMobile

- Build nimble-ui-automation solution

- Open Test Explore from Menu --> Test --> Windows --> Text Explorer

- Select Required test

- Login to Browser Stack to view the execution

**BrowserStack Credientials:**

**URL:** https://www.browserstack.com/automate

**UserName:** Spottlacher

**Password:** Cigniti@123
