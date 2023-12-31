﻿namespace eStoreClient.Controllers;

using System.Net.Http.Headers;
using System.Text.Json;
using BusinessObject;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

public class MemberController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private string _baseUrl = "http://localhost:5172/Member";

    public MemberController()
    {
        this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    #region GET

    private readonly MemberRepository memberRepository = new MemberRepository();
    
    public async Task<IActionResult> Index()
    {
        var list = this.memberRepository.GetAllMembers();
        return View(list);
    }

    public ActionResult Detail(int? id)
    {
        if (id == null || id <= 0)
            return this.NotFound();
        var member = this.memberRepository.GetMemberById(id.Value);
        if (member == null)
            return this.NotFound();
        return this.View(member);
    }

    public ActionResult Edit(int id)
    {
        if (id == null || id <= 0)
            return this.NotFound();
        var member = this.memberRepository.GetMemberById(id);
        if (member == null)
            return this.NotFound();
        return this.View(member);
    }

    public ActionResult Delete(int id)
    {
        if (id == null || id <= 0)
            return this.NotFound();
        var member = this.memberRepository.GetMemberById(id);
        if (member == null)
            return this.NotFound();
        return this.View(member);
    }
    public ActionResult Create() { return this.View(); }

    #endregion

    #region CUD

    [HttpPost] [ValidateAntiForgeryToken] public async Task<IActionResult> Create(Member member)
    {
        this.memberRepository.AddMember(member);
        return this.RedirectToAction("Index");
    }

    [HttpPost] [ValidateAntiForgeryToken] public async Task<IActionResult> Edit(Member member)
    {
        this.memberRepository.UpdateMember(member);
        return this.RedirectToAction("Index");
    }

    [HttpPost] [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Member member)
    {
        this.memberRepository.DeleteMember(member);
        return this.RedirectToAction("Index");
    }

    #endregion
}