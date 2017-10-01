using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameCapturer;

namespace FrameCapturer.Test
{
    /// <summary>
    /// Test the functionality of FrameManager Class
    /// </summary>
    [TestClass]
    public class FrameManagerTest
    {
        public FrameManagerTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        /// Get test context
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
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void AddFrameTest()
        {
            // test case 1 (Add 11 frames, which is less than target_legnth_max, then add another frame)
            FrameManager fm = new FrameManager(10);
            for (int i = 0; i < 11; ++i)
            {
                Frame newF = new Frame { frameId = i + 1, frameResource = "Placeholder Content" };
                fm.AddFrame(newF);
            }

            var actual = fm.StoredFrameCount;
            var expect = 11;
            Assert.AreEqual(expect, actual);

            // Add a frame
            fm.AddFrame(new Frame { frameId = 12, frameResource = "Placeholder Content" });
            actual = fm.StoredFrameCount;
            expect = 12;
            Assert.AreEqual(expect, actual);

            // test case 2 (Add number of frames equal to target_length_max)
            fm = new FrameManager(5);
            for (int i = 0; i < 6; ++i)
            {
                Frame newF = new Frame { frameId = i + 1, frameResource = "Placeholder Content" };
                fm.AddFrame(newF);
            }

            actual = fm.StoredFrameCount;
            expect = 6;
            Assert.AreEqual(expect, actual);

            // test case 3, Add number of frames larger than target_length_max
            fm = new FrameManager(5);
            for (int i = 0; i < 7; ++i)
            {
                Frame newF = new Frame { frameId = i + 1, frameResource = "Placeholder Content" };
                fm.AddFrame(newF);
            }

            actual = fm.StoredFrameCount;
            expect = 4;
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void GetFrameTest()
        {
            // multiple tests simulating different usage scenarios
            var fm = new FrameManager(5);
            for (int i = 0; i < 9; ++i)
            {
                Frame newF = new Frame { frameId = i + 1, frameResource = "Placeholder Content" };
                fm.AddFrame(newF);

                Frame[] expect;
                Frame[] actual;
                if (i + 1 == 6)
                {
                    expect = new Frame[6]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 2, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 4, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" },
                        new Frame { frameId = 6, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 7)
                {
                    expect = new Frame[4]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" },
                        new Frame { frameId = 7, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 9)
                {
                    expect = new Frame[5]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" },
                        new Frame { frameId = 7, frameResource = "Placeholder Content" },
                        new Frame { frameId = 9, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
            }

            // another test case with different target_length
            fm = new FrameManager(3);
            for (int i = 0; i < 9; ++i)
            {
                Frame newF = new Frame { frameId = i + 1, frameResource = "Placeholder Content" };
                fm.AddFrame(newF);

                Frame[] expect;
                Frame[] actual;
                if (i + 1 == 3)
                {
                    expect = new Frame[3]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 2, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 4)
                {
                    expect = new Frame[4]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 2, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 4, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 5)
                {
                    expect = new Frame[3]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 6)
                {
                    expect = new Frame[3]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 7)
                {
                    expect = new Frame[4]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 3, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" },
                        new Frame { frameId = 7, frameResource = "Placeholder Content" }

                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
                else if (i + 1 == 9)
                {
                    expect = new Frame[3]{
                        new Frame { frameId = 1, frameResource = "Placeholder Content" },
                        new Frame { frameId = 5, frameResource = "Placeholder Content" },
                        new Frame { frameId = 9, frameResource = "Placeholder Content" }
                    };
                    actual = (Frame[])fm.GetFrame();
                    CollectionAssert.AreEqual(expect, actual);
                }
            }
        }
    }
}
