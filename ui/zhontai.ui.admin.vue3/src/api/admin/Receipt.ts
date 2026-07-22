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
  PageInputReceiptEntity,
  ReceiptEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputReceiptEntity,
  ResultOutputReceiptEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class ReceiptApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags receipt
   * @name GetPage
   * @request POST:/api/admin/receipt/get-page
   * @secure
   */
  getPage = (data: PageInputReceiptEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputReceiptEntity, any>({
      path: `/api/admin/receipt/get-page`,
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
   * @tags receipt
   * @name Get
   * @request GET:/api/admin/receipt/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputReceiptEntity, any>({
      path: `/api/admin/receipt/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags receipt
   * @name Add
   * @request POST:/api/admin/receipt/add
   * @secure
   */
  add = (data: ReceiptEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/receipt/add`,
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
   * @tags receipt
   * @name Update
   * @request PUT:/api/admin/receipt/update
   * @secure
   */
  update = (data: ReceiptEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/receipt/update`,
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
   * @tags receipt
   * @name Delete
   * @request DELETE:/api/admin/receipt/delete
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
      path: `/api/admin/receipt/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags receipt
   * @name BatchDelete
   * @request PUT:/api/admin/receipt/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/receipt/batch-delete`,
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
   * @tags receipt
   * @name Audit
   * @summary 审核收款单 — 核销销货单的应收账款
   * @request PUT:/api/admin/receipt/audit
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
      path: `/api/admin/receipt/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags receipt
   * @name UnAudit
   * @summary 反审核
   * @request PUT:/api/admin/receipt/un-audit
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
      path: `/api/admin/receipt/un-audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
