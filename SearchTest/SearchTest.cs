using FundHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SearchTest
{
    
    
    /// <summary>
    ///这是 SearchTest 的测试类，旨在
    ///包含所有 SearchTest 单元测试
    ///</summary>
    [TestClass()]
    public class SearchTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Search 构造函数 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void SearchConstructorTest()
        {
            Search_Accessor target = new Search_Accessor();
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///Search SearchByCode的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void Search_SearchByCodeTest1()
        {
            Search_Accessor target = new Search_Accessor();
            WebService web = new WebService();
            List<Fund> list = web.GetAllFund();
            Assert.AreNotEqual(0, target.SearchByCode(list, "000000"));
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///Search SearchByCode的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void Search_SearchByCodeTest2()
        {
            Search_Accessor target = new Search_Accessor();
            WebService web = new WebService();
            List<Fund> list = web.GetAllFund();
            int i = target.SearchByCode(list, "000003");
            Assert.AreEqual(i, 1);
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///Search SearchByName的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void Search_SearchByNameTest1()
        {
            Search_Accessor target = new Search_Accessor();
            WebService web = new WebService();
            List<Fund> list = web.GetAllFund();
            Assert.AreEqual(0, target.SearchByName(list, "华夏成长"));
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///Search SearchByName的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void Search_SearchByNameTest2()
        {
            Search_Accessor target = new Search_Accessor();
            WebService web = new WebService();
            List<Fund> list = web.GetAllFund();
            int i = target.SearchByName(list, "华夏成长");
            Assert.AreNotEqual(i, 1);
            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }


        /// <summary>
        ///compar 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("FundHelper.exe")]
        public void comparTest()
        {
            Fund f1 = new Fund(); // TODO: 初始化为适当的值
            Fund f2 = new Fund(); // TODO: 初始化为适当的值
            Fund f3 = new Fund();
            f1.Name = "华夏成长";
            f2.Name = "华夏成长";
            f3.Name = "敏捷开发";
            List<int> expected = new List<int>(); // TODO: 初始化为适当的值
            expected.Add(-1);
            expected.Add(0);
            expected.Add(1);
            List<int> actual = new List<int>();
            //actual = Search_Accessor.compar(f1, f2);

            if (f1.Name.CompareTo(f3.Name) < 0)
                actual.Add(-1);
            if (f1.Name == f2.Name)
                actual.Add(0);
            if (f3.Name.CompareTo(f2.Name) > 0)
                actual.Add(1);

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }
    }
}
