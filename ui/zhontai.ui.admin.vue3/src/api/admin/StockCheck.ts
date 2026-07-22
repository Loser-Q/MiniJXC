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

import {
  PageInputStockCheckEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputStockCheckEntity,
  ResultOutputStockCheckEntity,
  StockCheckEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class StockCheckApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags stock-check
   * @name GetPage
   * @request POST:/api/admin/stock-check/get-page
   * @secure
   */
  getPage = (data: PageInputStockCheckEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputStockCheckEntity, any>({
      path: `/api/admin/stock-check/get-page`,
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
   * @tags stock-check
   * @name Get
   * @request GET:/api/admin/stock-check/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputStockCheckEntity, any>({
      path: `/api/admin/stock-check/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags stock-check
   * @name Add
   * @request POST:/api/admin/stock-check/add
   * @secure
   */
  add = (data: StockCheckEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/stock-check/add`,
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
   * @tags stock-check
   * @name Update
   * @request PUT:/api/admin/stock-check/update
   * @secure
   */
  update = (data: StockCheckEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/stock-check/update`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags stock-check
   * @name Delete
   * @request DELETE:/api/admin/stock-check/delete
   * @secure
   */
  delete = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/stock-check/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags stock-check
   * @name Audit
   * @summary 审核盘点单 — 盘盈入库 / 盘亏出库
   * @request PUT:/api/admin/stock-check/audit
   * @secure
   */
  audit = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/stock-check/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
