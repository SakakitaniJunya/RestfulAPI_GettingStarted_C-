using ContosoPizza.Models;
using ContosoPizza.Service;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Contrillers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {

    }

    /// <summary>
    ///  Get All Data
    /// </summary>
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();

    /// <summary>
    /// Select Get Data 
    /// </summary>
    /// <param name="id"></param>
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);

        // error handle
        if (pizza == null)
            return NotFound();

        return pizza;
    }

    /// <summary>
    /// Postメソッド：データを登録する
    /// </summary>
    /// <param name="pizza">登録したいピザ情報</param>
    /// <returns>登録結果を返す</returns>
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }
    // 第1引数 nameofはメソッド名をそのまま文字列として返す
    // 第2引数 URL生成時に使用するコントローラの名前
    // 第3引数 登録オブジェクト

    /// <summary>
    /// Value Update 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pizza"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
        // 既に登録されているか確認 Already
        var existingPizza = PizzaService.Get(id);
        if (existingPizza is null)
            return NotFound();

        PizzaService.Update(pizza);

        return NoContent();
    }

    /// <summary>
    /// Pizza Delete  
    /// </summary>
    /// <param name="id">Pizza Info Index</param>
    /// <returns> Action Result</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);

        if (pizza is null)
            return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}