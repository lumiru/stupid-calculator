using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StupidCalculatorLib;

namespace TestLib
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
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

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestAddition()
        {
            Operator op = new Operator();
            op.addNumber(1010);
            op.operation = Operation.ADD;
            op.addNumber(2003);
            Assert.AreEqual(3013, op.compute());
            op.operation = Operation.ADD;
            op.addNumber(100);
            Assert.AreEqual(3113, op.compute());
        }

        [TestMethod]
        public void TestSubstract()
        {
            Operator op = new Operator();
            op.addNumber(3013);
            op.operation = Operation.SUBSTRACT;
            op.addNumber(2003);
            Assert.AreEqual(1010, op.compute());
            op.operation = Operation.SUBSTRACT;
            op.addNumber(30);
            Assert.AreEqual(980, op.compute());
        }

        [TestMethod]
        public void TestMultiply()
        {
            Operator op = new Operator();
            op.addNumber(1010);
            op.operation = Operation.MULTIPLY;
            op.addNumber(4);
            Assert.AreEqual(4040, op.compute());
            op.operation = Operation.MULTIPLY;
            op.addNumber(2);
            Assert.AreEqual(8080, op.compute());
        }

        [TestMethod]
        public void TestDivide()
        {
            Operator op = new Operator();
            op.addNumber(4040);
            op.operation = Operation.DIVIDE;
            op.addNumber(4);
            Assert.AreEqual(1010, op.compute());
            op.operation = Operation.DIVIDE;
            op.addNumber(10);
            Assert.AreEqual(101, op.compute());
        }
    }
}
