using System;
using System.Collections.Generic;
using NUnit.Framework;
using RefactorThis.Persistence;
using RefactorThis.Persistence.Constant;
using RefactorThis.Persistence.Interface;

namespace RefactorThis.Domain.Tests
{
	[TestFixture]
	public class InvoicePaymentProcessorTests
	{
		[Test]
		public void ProcessPayment_Should_ThrowException_When_NoInoiceFoundForPaymentReference( )
		{
            var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( );
			var failureMessage = "";
			try
			{
				var result = paymentProcessor.ProcessPayment( payment );
			}
			catch ( InvalidOperationException e )
			{
				failureMessage = e.Message;
			}
			Assert.AreEqual(ResponseMessage.NoInvMatch, failureMessage );
		}
		[Test]
		public void ProcessPayment_Should_ReturnFailureMessage_When_NoPaymentNeeded( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 0,
				AmountPaid = 0,
				Payments = null
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( );
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.NoPayNeed, result );
		}
		[Test]
		public void ProcessPayment_Should_ReturnFailureMessage_When_InvoiceAlreadyFullyPaid( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 10,
				Payments = new List<Payment>
				{
					new Payment
					{
						Amount = 10
					}
				}
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( );
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.InvWasFullyPaid, result );
		}
		[Test]
		public void ProcessPayment_Should_ReturnFailureMessage_When_PartialPaymentExistsAndAmountPaidExceedsAmountDue( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 5,
				Payments = new List<Payment>
				{
					new Payment
					{
						Amount = 5
					}
				}
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 6
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.GreaterPartialAmt, result );
		}

		[Test]
		public void ProcessPayment_Should_ReturnFailureMessage_When_NoPartialPaymentExistsAndAmountPaidExceedsInvoiceAmount( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 5,
				AmountPaid = 0,
				Payments = new List<Payment>( )
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 6
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.GreaterInvAmt, result );
		}

		[Test]
		public void ProcessPayment_Should_ReturnFullyPaidMessage_When_PartialPaymentExistsAndAmountPaidEqualsAmountDue( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 5,
				Payments = new List<Payment>
				{
					new Payment
					{
						Amount = 5
					}
				}
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 5
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.FinPayRecv, result );
		}

		[Test]
		public void ProcessPayment_Should_ReturnFullyPaidMessage_When_NoPartialPaymentExistsAndAmountPaidEqualsInvoiceAmount( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 0,
				Payments = new List<Payment>( ) { new Payment( ) { Amount = 10 } }
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 10
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.InvWasFullyPaid, result );
		}

		[Test]
		public void ProcessPayment_Should_ReturnPartiallyPaidMessage_When_PartialPaymentExistsAndAmountPaidIsLessThanAmountDue( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 5,
				Payments = new List<Payment>
				{
					new Payment
					{
						Amount = 5
					}
				}
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 1
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.PartialPayRecv, result );
		}

		[Test]
		public void ProcessPayment_Should_ReturnPartiallyPaidMessage_When_NoPartialPaymentExistsAndAmountPaidIsLessThanInvoiceAmount( )
		{
			var repo = new InvoiceRepository( );
            var payProcessor = new PaymentProcessor();
            var invoice = new Invoice( repo )
			{
				Amount = 10,
				AmountPaid = 0,
				Payments = new List<Payment>( )
			};
			repo.Add( invoice );
			var paymentProcessor = new InvoiceService( repo, payProcessor);
			var payment = new Payment( )
			{
				Amount = 1
			};
			var result = paymentProcessor.ProcessPayment( payment );
			Assert.AreEqual(ResponseMessage.InvPartiallyPaid, result );
		}
	}
}