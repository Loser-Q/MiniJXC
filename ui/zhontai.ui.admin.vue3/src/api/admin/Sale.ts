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
  PageInputSaleEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputSaleEntity,
  ResultOutputSaleEntity,
  SaleEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class SaleApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags sale
   * @name GetPage
   * @request POST:/api/admin/sale/get-page
   * @secure
   */
  getPage = (data: PageInputSaleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputSaleEntity, any>({
      path: `/api/admin/sale/get-page`,
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
   * @tags sale
   * @name Get
   * @request GET:/api/admin/sale/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputSaleEntity, any>({
      path: `/api/admin/sale/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags sale
   * @name Add
   * @request POST:/api/admin/sale/add
   * @secure
   */
  add = (data: SaleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/sale/add`,
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
   * @tags sale
   * @name Update
   * @request PUT:/api/admin/sale/update
   * @secure
   */
  update = (data: SaleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/sale/update`,
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
   * @tags sale
   * @name Delete
   * @request DELETE:/api/admin/sale/delete
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
      path: `/api/admin/sale/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags sale
   * @name BatchDelete
   * @request PUT:/api/admin/sale/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/sale/batch-delete`,
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
   * @tags sale
   * @name Audit
   * @request PUT:/api/admin/sale/audit
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
      path: `/api/admin/sale/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags sale
   * @name UnAudit
   * @request PUT:/api/admin/sale/un-audit
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
      path: `/api/admin/sale/un-audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
