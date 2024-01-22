# Замечания по проекту

* [x] Для сущности Комментарии нужно создать 2 таблицы - для Комментариев для Фрилансера/Заказчика и для Заказа
* [x] В Регистрации нет PasswordConfirm
* [x] Изменил Заказ - время выбирать не Старт и Конец, а за сколько Дней выполняется
* Добавить прикрепление файлов к заказу
* Внедрить Mapster
* В чем разница между PhotoViewModel и AvatarViewModel

public async Task<List<Order>> GetOrdersByAllTags(string[] tags)
{
    return await _context.Orders
        .Where(o => tags.All(t => o.OrderTags.Any(ot => ot.Tag.Name == t)))
        .ToListAsync();
}

[HttpPost("{orderId}/tags")]
public async Task<ActionResult> AddTagToOrder(int orderId, TagDto tagDto)
{
    // Реализация добавления тега к заказу
}

[HttpGet("search")]
public async Task<ActionResult<IEnumerable<Order>>> SearchOrdersByTags([FromQuery] string[] tags)
{
    // Реализация поиска заказов по тегам
}