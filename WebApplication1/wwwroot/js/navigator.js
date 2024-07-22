async function NavigateBack()
{
    await NavigateTo(!document.referrer ? "/" : document.referrer);
}

async function NavigateTo(url)
{
    //получаем html тело
    const response = await fetch(url);
    if(!response.ok)
        throw Error("Что не так с ссылкой!")
    const htmlResponse = await response.text();
    //магия чтобы html текст превратить в объект js
    const htmlElement = document.createElement("html");
    htmlElement.innerHTML = htmlResponse;
    
    //из js объекта получаем body
    const newBodyHtml = htmlElement.getElementsByTagName("body")[0];
    
    //получаем заголовок с сервера
    const newTitleHtml = htmlElement.getElementsByTagName("title")[0].innerText;
    
    //текущее body
    const currentHtmlBody = document.getElementsByTagName("body")[0];
    
    //из старого тела получаем скрипты и удаляем их чтобы их вставить по другому чтобы они работали
    const scripts = newBodyHtml.querySelectorAll("script");
    scripts.forEach(script => {
        script.remove();
    })
    
    //вставляем новое body
    currentHtmlBody.innerHTML = newBodyHtml.innerHTML;
    //меняем историю в браузере
    window.history.pushState({}, newTitleHtml, url);
    //добавляем скрипты с сервера
    scripts.forEach(script => {
        const newScript = document.createElement("script");
        newScript.src = script.src;
        document.body.appendChild(newScript);
    })
}