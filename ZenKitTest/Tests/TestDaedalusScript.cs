using NUnit.Framework;
using ZenKit;

namespace ZenKitTest.Tests
{
	public class TestDaedalusScript
	{
		[Test]
		public void TestLoad()
		{
			var scr = new DaedalusScript("./Samples/menu.proprietary.dat");

			var syms = scr.Symbols;
			Assert.That(syms, Has.Count.EqualTo(1093));
			Assert.That(scr.SymbolCount, Is.EqualTo(1093));

			var classSymbol = scr.GetSymbolByIndex(118);
			var prototypeSymbol = scr.GetSymbolByIndex(133);
			var externalSymbol = scr.GetSymbolByIndex(1);
			var memberSymbol = scr.GetSymbolByName("C_MENU.BACKPIC");
			var instanceSymbol = scr.GetSymbolByName("MENU_MAIN");
			var functionSymbol = scr.GetSymbolByAddress(1877);

			var nonexistentSymbol1 = scr.GetSymbolByIndex((uint)(syms.Count + 100));
			var nonexistentSymbol2 = scr.GetSymbolByName("nonexistent_lol");
			var nonexistentSymbol3 = scr.GetSymbolByAddress(0xffffffaa);

			Assert.That(classSymbol, Is.Not.Null);
			Assert.That(memberSymbol, Is.Not.Null);
			Assert.That(prototypeSymbol, Is.Not.Null);
			Assert.That(instanceSymbol, Is.Not.Null);
			Assert.That(functionSymbol, Is.Not.Null);
			Assert.That(externalSymbol, Is.Not.Null);
			Assert.That(nonexistentSymbol1, Is.Null);
			Assert.That(nonexistentSymbol2, Is.Null);
			Assert.That(nonexistentSymbol3, Is.Null);

			Assert.That(classSymbol!.Name, Is.EqualTo("C_MENU"));
			Assert.That(classSymbol.Size, Is.EqualTo(13));
			Assert.That(classSymbol.Type, Is.EqualTo(DaedalusDataType.Class));
			Assert.That(classSymbol.HasReturn, Is.False);

			Assert.That(functionSymbol!.Name, Is.EqualTo("SHOWINTRO"));
			Assert.That(functionSymbol.Size, Is.EqualTo(0));
			Assert.That(functionSymbol.Address, Is.EqualTo(1877));
			Assert.That(functionSymbol.Type, Is.EqualTo(DaedalusDataType.Function));
			Assert.That(functionSymbol.ReturnType, Is.EqualTo(DaedalusDataType.Int));
			Assert.That(functionSymbol.HasReturn, Is.True);
		}
	}
}