function jsonPriceListVariants ()
{
   //получаем типы колонок которые вставляется в виде json в div в аттрибут
   return document
       .querySelector(".price-list-columns")
       .attributes
       .getNamedItem("column-type-variants")
       .value;   
}

function priceListVariants ()
{
   return JSON.parse(jsonPriceListVariants());
}

function priceListColumns ()
{
   return document.querySelector(".create-price-list-form .price-list-columns tbody");
}

function AddButtonEvent()
{
   //количество колонок (не html таблицы, а колонок из прайс-листа)
   const columnCount = priceListColumns().childElementCount;
   //в этом tr будет весь контент в резульате
   const tr = document.createElement("tr");
   //делаем первый td который содержит счетчик количества колонок создаваемого прайс-листа
   const tdFirst = document.createElement("td");
   tdFirst.innerHTML = `<div class="p-3 bg-gray-100 rounded">#${columnCount + 1}</div>`;
   //второй td содержит input куда пользователь вводит название колонки 
   const tdSecond = document
       .createElement("td");
   tdSecond.innerHTML = `<input class="p-3 bg-gray-100 text-gray-700 hover:bg-gray-200 font-bold rounded w-full" />`;
   //третий td содержит варианты выбора типа данных
   const tdThird = document.createElement("td");
   const tdThirdSelect = document.createElement("select");
   tdThirdSelect.className = "p-3 bg-gray-100 rounded";
   tdThird.append(tdThirdSelect);
   //заполняем select варинатами ответа
   priceListVariants().forEach(priceListVariant => {
      const optionElement = document.createElement("option");
      optionElement.value = priceListVariant.Id;
      optionElement.innerText = priceListVariant.Name;
      tdThirdSelect.append(optionElement);
   });
   //td с кнпокой удалить
   const tdFourth = document.createElement("td");
   tdFourth.innerHTML = `<button onclick="RemoveDeleteButton(event)" class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">Удалить</button>`;

   //вставляем все созданные td
   tr.append(tdFirst);
   tr.append(tdSecond);
   tr.append(tdThird);
   tr.append(tdFourth);
   //добавляем тот tr из начала
   priceListColumns().append(tr);
}


function CreatePriceListColumn()
{
   //нулевой элемент название прайс-листа, остальное это колонки
   //input => value
   const priceListName = document.querySelector(".create-price-name").value;
   const newColumns = [];
   for (let i = 1; i < priceListColumns().children.length; i++)
   {
      const priceListTr = priceListColumns().children[i];
      //первый td содержит число-итератор а не данные поэтому данные НЕ с 0 элемента
      const inputPropName = priceListTr.children[1].firstChild.value;
      const inputPropType = priceListTr.children[2].firstChild.value;
      const columnData = {
         PropName: inputPropName,
         PropTypeId: Number.parseInt(inputPropType)
      }
      newColumns.push(columnData);
   }
   const createPriceListData = {
      Name:priceListName,
      ExistingColumns: [],
      NewPriceColumns: newColumns
   };
   pricelisthub.invoke("CreatePriceList", createPriceListData).then(() => {
      NavigateTo("/price-lists")
   });
}


function RemoveDeleteButton (event) {
   //удаляем tr в котором находится button
   event.currentTarget.parentElement.parentElement.remove();
   //переписываем значение итератора в таблице
   //скрипт из table-order.js
   ReNumberTable();
}