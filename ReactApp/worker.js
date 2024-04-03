class worker{
    async stealdata(){
        const browser = await puppeteer.launch({headless:false})
        const page = await browser.newPage()
        await page.goto('https://magnit.ru/catalog/?categoryId=4854')
    
        let cost = await page.evaluate(() => {
            let text = document.querySelector('.new-card-product__price-regular').textContent
            return text
        })
        await browser.close()
        return cost
    }
}
