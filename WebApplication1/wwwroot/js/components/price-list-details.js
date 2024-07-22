function deleteProduct(event) {
    const productIdStr = event.currentTarget.attributes.getNamedItem("productId").value;
    const productId = Number.parseInt(productIdStr);
    pricelisthub.invoke("DeleteProduct", {ProductId:productId}).then(() => {
        const deleteButton = document.querySelector(`.products-table .delete-product[productId="${productIdStr}"]`);
        deleteButton.parentElement.parentElement.remove();
    });
}