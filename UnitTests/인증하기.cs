﻿using _2D보험구분검증툴;
using _2D보험구분검증툴.Class;
using _2D보험구분검증툴.Interface;
using _2D보험구분검증툴.Logic;
using _2D보험구분검증툴.Test;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static _2D보험구분검증툴.Test.TestEnum;

namespace UnitTests
{
    [TestFixture]
    class 인증하기
    {
        [SetUp]
        public void SetUp()
        {
            MessageHelper.IsTest = true;
        }

        [TearDown]
        public void TearDown()
        {
            MessageHelper.IsTest = false;
        }

        [Test]
        public void DLL이없을경우()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.IsAny<string>()))
                .Throws<DllNotFoundException>();

            frm.인증시도();
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.DLL없음));
        }

        [Test]
        public void 올바르게인증되는경우_파라미터99999999로넣었을경우()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.Is<string>(y => y == "99999999")))
                .Returns(true);

            frm.인증시도("99999999");
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.인증완료));
        }

        [Test]
        public void 올바르게인증되는경우_파라미터안넣고기본파라미터로했을경우()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.Is<string>(y => y == "99999999")))
                .Returns(true);

            frm.인증시도();
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.인증완료));
        }

        [Test]
        public void 인증실패_1()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.Is<string>(y => y != "99999999")))
                .Returns(false);

            frm.인증시도("333");
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.인증실패));
        }

        [Test]
        public void 인증실패_2()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.Is<string>(y => y != "99999999")))
                .Returns(false);

            frm.인증시도("as df ");
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.인증실패));
        }

        [Test]
        public void 인증실패_3()
        {
            var mock인증하기 = new Mock<I인증하기>();
            var frm = new Form1(null, mock인증하기.Object, null);

            mock인증하기.Setup(x => x.UB2DCheckAuthProcess(It.Is<string>(y => y != "99999999")))
                .Returns(false);

            frm.인증시도("asdf  33");
            Assert.That(frm._e인증결과, Is.EqualTo(e인증결과.인증실패));
        }
    }
}
