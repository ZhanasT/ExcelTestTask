//using ExcelTask.Core.Domain;

//public async Task<int> CreateGroupProducts()
//{
//    var unprocessedProducts = await GetUnprocessedProducts();
//    var newProductGroups = new List<ProductGroup>();
//    var newProductGroupRelations = new List<ProductGroupRelation>();
//    decimal currentGroupPrice = 0;
//    int currentGroupQuantity = 0;
//    int newProductGroupIndex = await _unitOfWork.ProductGroupRepository.GetLastIdAsync() + 1;

//    if (unprocessedProducts.Count == 0)
//    {
//        return 0;
//    }

//    var currentGroup = new ProductGroup { GroupName = $"Group {newProductGroupIndex}" };

//    foreach (var unprocessedProduct in unprocessedProducts)
//    {
//        while (unprocessedProduct.Quantity > 0)
//        {
//            if (currentGroupPrice + unprocessedProduct.UnitPrice > 200)
//            {
//                // Добавляем текущую группу товаров в коллекцию
//                newProductGroups.Add(currentGroup);

//                // Сохраняем связь между товаром и текущей группой в коллекцию
//                var newProductGroupRelation = new ProductGroupRelation
//                {
//                    ProductId = unprocessedProduct.Id,
//                    ProductGroupId = currentGroup.Id,
//                    QuantityInGroup = currentGroupQuantity
//                };
//                newProductGroupRelations.Add(newProductGroupRelation);

//                // Создаем новую группу товаров
//                newProductGroupIndex++;
//                currentGroup = new ProductGroup { GroupName = $"Group {newProductGroupIndex}" };
//                currentGroupPrice = 0;
//                currentGroupQuantity = 0;

//                continue;
//            }

//            // Вычисляем количество товара, которое можно добавить к текущей группе
//            int quantityToAdd = Math.Min(unprocessedProduct.Quantity, (int)((200 - currentGroupPrice) / unprocessedProduct.UnitPrice));
//            currentGroup.Products.Add(new Product
//            {
//                Id = unprocessedProduct.Id,
//                Name = unprocessedProduct.Name,
//                UnitOfMeasureTypeId = unprocessedProduct.UnitOfMeasureTypeId,
//                UnitOfMeasureType = unprocessedProduct.UnitOfMeasureType,
//                UnitPrice = unprocessedProduct.UnitPrice,
//                Quantity = quantityToAdd
//            });

//            // Увеличиваем текущую сумму и количество для текущей группы
//            currentGroupPrice += quantityToAdd * unprocessedProduct.UnitPrice;
//            currentGroupQuantity += quantityToAdd;

//            // Уменьшаем количество товара на количество добавленное к текущей группе
//            unprocessedProduct.Quantity -= quantityToAdd;
//        }
//    }

//    // Добавляем последнюю текущую группу товаров в коллекцию
//    if (currentGroup.Products.Any())
//    {
//        newProductGroups.Add(currentGroup);
//    }

//    // Сохраняем новые группы товаров и связи в базу данных
//    foreach (var productGroup in newProductGroups)
//    {
//        await _unitOfWork.ProductGroupRepository.AddAsync(productGroup);
//    }

//    foreach (var productGroupRelation in newProductGroupRelations)
//    {
//        await _unitOfWork.ProductGroupRelationRepository.AddAsync(productGroupRelation);
//    }

//    return newProductGroups.Count;
//}
