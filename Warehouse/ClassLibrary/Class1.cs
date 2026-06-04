namespace ClassLibrary
{
    public class ClassGoods
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public decimal NettoPrice { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int? TypeOfGoodId { get; set; }
        public virtual ClassTypeOfGood? TypeOfGood { get; set; }
        public int? SupplierId { get; set; }
        public virtual ClassSupplier? Supplier { get; set; }
    }

    public class ClassTypeOfGood
    {
        public int Id { get; set; }
        public string? Name { set; get; }
        public virtual ICollection<ClassGoods>? Goods { get; set; } = new List<ClassGoods>();
    }

    public class ClassSupplier
    {
        public int Id { set; get; }
        public string? Name { get; set; }
        public virtual ICollection<ClassGoods> Goods { get; set; } = new List<ClassGoods>();
    }
}
