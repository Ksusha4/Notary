using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using WindowsFormsApp2;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Form form = new Form();

            // Act

            // Assert
            Assert.IsNotNull(form.Controls);
            Assert.IsNotNull(form.Size);
            Assert.IsNotNull(form.BackColor);
        }

        [TestMethod]
        public void TestInputValidation()
        {
            // Arrange
            Form form = new Form();
            TextBox textBoxPhone = new TextBox();
            form.Controls.Add(textBoxPhone);
            string invalidPhoneNumber = "1234567890abc";

            // Act
            textBoxPhone.Text = invalidPhoneNumber;
            form.Validate();

            // Assert
            Assert.IsFalse(textBoxPhone.Text.Length == 0);
        }

        [TestMethod]
        public void TestFormClosingEvent()
        {
            // Arrange
            var form = new Form1();

            // Act
            form.Close(); // Триггер закрытия формы

            // Assert
            Assert.IsFalse(form.Visible);
        }

        [TestMethod]
        public void TestFormInitialization()
        {
            // Arrange
            var form = new Form1();

            // Act

            // Assert
            Assert.IsNotNull(form); // Проверяем, что форма была успешно создана
            Assert.AreEqual("Авторизация", form.Text); // Проверяем, что заголовок формы установлен корректно
        }
    }
}