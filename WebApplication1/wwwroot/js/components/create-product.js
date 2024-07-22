function сreateProduct() {
    const productName = document.querySelector(".product-creation-container .product-name-input").value;
    const productArticul = document.querySelector(".product-creation-container .product-articul-input").value;
    const columnValueElements = document.querySelectorAll(".product-creation-container .column-value");
    const columnValues = [];
    columnValueElements.forEach(
        element => {
            const value = element.value.toString();
            const attributes = element.attributes;
            const columnName = attributes.getNamedItem("columnName").value;
            const columnTypeStr = attributes.getNamedItem("columnType").value;
            const columnType = Number.parseInt(columnTypeStr);
            columnValues.push({
                ColumnName: columnName,
                ColumnTypeId: columnType,
                Value: value
            })
        }
    );
    const priceListStr = document
        .querySelector(".product-creation-container")
        .attributes
        .getNamedItem("priceListId")
        .value;
    const priceListId = Number.parseInt(priceListStr);
    const createProductData = {
        Name: productName,
        Articul: productArticul,
        ColumnsData: columnValues,
        PriceListId: priceListId
    };
    pricelisthub.invoke("CreateProduct", createProductData).then(() => {
        NavigateTo(`/price-list/${priceListId}`)
    });
}