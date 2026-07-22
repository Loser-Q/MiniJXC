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
  PageInputPaymentEntity,
  PaymentEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputPaymentEntity,
  ResultOutputPaymentEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class PaymentApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags payment
   * @name GetPage
   * @request POST:/api/admin/payment/get-page
   * @secure
   */
  getPage = (data: PageInputPaymentEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputPaymentEntity, any>({
      path: `/api/admin/payment/get-page`,
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
   * @tags payment
   * @name Get
   * @request GET:/api/admin/payment/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputPaymentEntity, any>({
      path: `/api/admin/payment/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags payment
   * @name Add
   * @request POST:/api/admin/payment/add
   * @secure
   */
  add = (data: PaymentEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/payment/add`,
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
   * @tags payment
   * @name Update
   * @request PUT:/api/admin/payment/update
   * @secure
   */
  update = (data: PaymentEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/payment/update`,
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
   * @tags payment
   * @name Delete
   * @request DELETE:/api/admin/payment/delete
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
      path: `/api/admin/payment/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags payment
   * @name BatchDelete
   * @request PUT:/api/admin/payment/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/payment/batch-delete`,
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
   * @tags payment
   * @name Audit
   * @summary 审核付款单 — 核销购货单的应付账款
   * @request PUT:/api/admin/payment/audit
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
      path: `/api/admin/payment/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags payment
   * @name UnAudit
   * @request PUT:/api/admin/payment/un-audit
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
      path: `/api/admin/payment/un-audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
