using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Testt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            string link = @"https://www.beymen.com/";
            driver.Navigate().GoToUrl(link);
            Thread.Sleep(5000);

            driver.FindElement(By.ClassName("bwi-account-o")).Click();
            driver.FindElement(By.Id("email")).SendKeys("Epostız");
            driver.FindElement(By.Id("password")).SendKeys("şifreniz");
            Thread.Sleep(1500);
            driver.FindElement(By.Id("loginBtn")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.ClassName("icon-favorite")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.ClassName("bwi-cart-o")).Click();

            Thread.Sleep(3000);

            driver.Navigate().GoToUrl(link);

            Thread.Sleep(3000);

            driver.FindElement(By.ClassName("o-header__search--input")).SendKeys("pantolan");
            Thread.Sleep(3000);

            driver.FindElement(By.ClassName("bmi-search")).Click();
            Thread.Sleep(3000);

            
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 5460)");
            Thread.Sleep(3000);

            driver.FindElement(By.Id("moreContentButton")).Click();

            int productCount = 0;
            int productBodySizeCount = 0;

            IReadOnlyCollection<IWebElement> protucts = driver.FindElements(By.ClassName("m-productCard__title"));
            

            foreach (IWebElement product in protucts)
            {
                productCount ++;


                if (productCount == 30)
                {
                    Random rndp = new Random();
                    int rndProductNo = rndp.Next(2, productCount);

                    IWebElement webProductElement = driver.FindElement(By.XPath($"//*[@id='productList']/div[{rndProductNo}]"));
                    webProductElement.Click();
                    Thread.Sleep(1500);

                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 300)");
                    Thread.Sleep(2000);

                    IReadOnlyCollection<IWebElement> productBodySizes = driver.FindElements(By.ClassName("m-variation__item"));

                    foreach (IWebElement productBodySize in productBodySizes)
                    {
                        productBodySizeCount ++;
                        //Console.WriteLine(productBodySize.Text);
                        //Console.WriteLine("***************************");
                    }
                    Random rndpbs = new Random();
                    int rndProductBodySize = rndpbs.Next(1, productBodySizeCount);

                    IWebElement webProductBodySizeElement = driver.FindElement(By.XPath($"//*[@id='sizes']/div/span[{rndProductBodySize}]"));

                    
                   
                        webProductBodySizeElement.Click();
                        Thread.Sleep(2000);



                    driver.FindElement(By.Id("addBasket")).Click();
                    Thread.Sleep(2000);

                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");
                    Thread.Sleep(2000);

                    IWebElement webProductPrice = driver.FindElement(By.XPath("//*[@id='priceNew']"));
                    //Console.WriteLine(webProductPrice.Text);
                    Thread.Sleep(2000);
                    string detailPrice = webProductPrice.Text; //Ürün Sayfasındaki Fiyat
                    string[] prices1;
                    string[] p;
                    string prices2;
                    prices1 = webProductPrice.Text.Split(',');
                    prices2 = prices1[1];
                    p = prices2.Split(' ');
                    string totalProductPrice = prices1[0] +'.'+ p[0];
                    

                    Thread.Sleep(2000);
                    driver.FindElement(By.ClassName("bwi-cart-o")).Click();//Sepet Sayfasına Yönlendirme
                    Thread.Sleep(2000);

                    IWebElement cartProductPrice = driver.FindElement(By.ClassName("m-productPrice__salePrice"));
                    string cartPrice = cartProductPrice.Text; //Sepet Sayfasındaki Fiyat
                    string[] carPrices1;
                    string[] pCard;
                    string carPrices2;
                    carPrices1 = cartProductPrice.Text.Split(',');
                    carPrices2 = carPrices1[1];
                    pCard = prices2.Split(' ');
                    string totalProductPriceCard = carPrices1[0] + '.' + pCard[0];

                    //Console.Write(totalProductPriceCard);

                    Thread.Sleep(2000);

                    if (double.Parse(totalProductPrice) == double.Parse(totalProductPriceCard))
                    {
                        Console.WriteLine("Fiyatlar Eşleşti!");
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='quantitySelect0']/option[2]")).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.Id("removeCartItemBtn0")).Click();
                        Console.WriteLine("Sepet Boşaltıldı!");
                        
                    }
                    else
                    {
                        Console.Write("Fiyatlar Eş Değil!");
                    }

                    

                }

            }

        }
    }
}
