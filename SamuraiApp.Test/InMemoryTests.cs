using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.Test
{
  [TestClass]
  public class InMemoryTests
  {
    [TestMethod]
    public void CanInsertSamurai()
    {
      var builder = new DbContextOptionsBuilder();
      builder.UseInMemoryDatabase("CanInsertSamurai");
      using (var context = new SamuraiContext(builder.Options))
      {
        var samurai = new Samurai();
        context.Samurais.Add(samurai);
        Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
      }
    }
  }
}