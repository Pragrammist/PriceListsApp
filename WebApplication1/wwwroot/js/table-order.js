function OrderColumn (event) {
    const elementIndex = getElementIndex(event.currentTarget);
    const orderedAttribute = event.currentTarget.attributes.getNamedItem("ordered");
    const hasOrdered = !!orderedAttribute;

    if(hasOrdered)
    {
        event.currentTarget.attributes.removeNamedItem("ordered")
    }
    else {
        event.currentTarget.attributes.setNamedItem(document.createAttribute("ordered"));
    }

    const trParent = event.currentTarget.parentElement.parentElement.parentElement.querySelector("tbody");
    const copiedElementToSort = Array.from(trParent.children);
    copiedElementToSort.sort(function (el1, el2) {
        const el1Value = el1.children[elementIndex].innerText.toLowerCase();
        const el2Value = el2.children[elementIndex].innerText.toLowerCase();

        if(el1Value === el2Value)
            return 0;
        
        
        if(+el1Value && +el2Value)
        {
            if(!hasOrdered)
                return +el1Value - +el2Value;
            else
                return +el2Value - +el1Value;
        }
        
        let result;
        if(!hasOrdered)
            result = el1Value < el2Value ? -1 : 1;
        else
            result = el1Value < el2Value  ? 1 : -1;
        return result;
    });
    const htmlOfEl = copiedElementToSort.map(el => el.outerHTML);
    for (let i = 0; i < trParent.children.length; i++)
    {
        const newHtml = htmlOfEl[i];
        const oldEl = trParent.children.item(i);
        oldEl.outerHTML = newHtml;
    }
    ReNumberTable();
}

function getElementIndex (element) {
    return Array.from(element.parentNode.children).indexOf(element);
}

function ReNumberTable(isReverse)
{
    const tbody = document.querySelector("tbody");
    
    Array.from(tbody.children).forEach((el,i) => {
        const tdEl = el.children[0];
        tdEl.innerHTML = tdEl.innerHTML.replace(/#\d+/, `#${i + 1}`);
    });
}