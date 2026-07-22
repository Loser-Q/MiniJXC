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
  PageInputPurchaseEntity,
  PurchaseEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputPurchaseEntity,
  ResultOutputPurchaseEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class PurchaseApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags purchase
   * @name GetPage
   * @request POST:/api/admin/purchase/get-page
   * @secure
   */
  getPage = (data: PageInputPurchaseEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputPurchaseEntity, any>({
      path: `/api/admin/purchase/get-page`,
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
   * @tags purchase
   * @name Get
   * @request GET:/api/admin/purchase/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputPurchaseEntity, any>({
      path: `/api/admin/purchase/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags purchase
   * @name Add
   * @request POST:/api/admin/purchase/add
   * @secure
   */
  add = (data: PurchaseEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/purchase/add`,
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
   * @tags purchase
   * @name Update
   * @request PUT:/api/admin/purchase/update
   * @secure
   */
  update = (data: PurchaseEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/purchase/update`,
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
   * @tags purchase
   * @name Delete
   * @request DELETE:/api/admin/purchase/delete
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
      path: `/api/admin/purchase/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags purchase
   * @name BatchDelete
   * @request PUT:/api/admin/purchase/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/purchase/batch-delete`,
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
   * @tags purchase
   * @name Audit
   * @summary 审核购货单 — 库存增加 + 产生应付
   * @request PUT:/api/admin/purchase/audit
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
      path: `/api/admin/purchase/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags purchase
   * @name UnAudit
   * @summary 反审核
   * @request PUT:/api/admin/purchase/un-audit
   * @secure
   */
  unAudit = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/purchase/un-audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
