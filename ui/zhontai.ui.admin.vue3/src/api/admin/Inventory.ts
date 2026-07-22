/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import { PageInputInventoryEntity, ResultOutputListInventoryBalanceOutput, ResultOutputPageOutputInventoryEntity } from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class InventoryApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags inventory
   * @name GetPage
   * @summary 库存流水列表
   * @request POST:/api/admin/inventory/get-page
   * @secure
   */
  getPage = (data: PageInputInventoryEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputInventoryEntity, any>({
      path: `/api/admin/inventory/get-page`,
      method: 'POST',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags inventory
   * @name GetBalance
   * @summary 当前库存查询（按商品+仓库汇总）
   * @request POST:/api/admin/inventory/get-balance
   * @secure
   */
  getBalance = (data: PageInputInventoryEntity, params: RequestParams = {}) =>
    this.request<ResultOutputListInventoryBalanceOutput, any>({
      path: `/api/admin/inventory/get-balance`,
      method: 'POST',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
}
