
# Тестовое задание
## Перед запуском

- Записать СonnectionString в appSetting.json или в user-secrets


## API Reference


### Загрузить список продуктов в формате excel

```http
  POST /api/Product/upload-products
```
- **ВАЖНО**: вещественные числа указывать через точку.
### Получить все группы продуктов

```http
  POST /api/ProductGroup/get
```

### Получить группу продуктов по идентификатору

```http
  POST /api/ProductGroup/get-by-id/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | **Required** |

## Запуск в Docker контейнере
Для запуска в Docker контейнере нужно выполнить в командной строке:
```bash
  docker compose up -d --build
```



