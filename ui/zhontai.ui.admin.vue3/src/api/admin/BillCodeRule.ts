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
  BillCodeRuleEntity,
  PageInputBillCodeRuleEntity,
  ResultOutputBillCodeRuleEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputBillCodeRuleEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class BillCodeRuleApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags bill-code-rule
   * @name GetPage
   * @request POST:/api/admin/bill-code-rule/get-page
   * @secure
   */
  getPage = (data: PageInputBillCodeRuleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputBillCodeRuleEntity, any>({
      path: `/api/admin/bill-code-rule/get-page`,
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
   * @tags bill-code-rule
   * @name Get
   * @request GET:/api/admin/bill-code-rule/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputBillCodeRuleEntity, any>({
      path: `/api/admin/bill-code-rule/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags bill-code-rule
   * @name Add
   * @request POST:/api/admin/bill-code-rule/add
   * @secure
   */
  add = (data: BillCodeRuleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/bill-code-rule/add`,
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
   * @tags bill-code-rule
   * @name Update
   * @request PUT:/api/admin/bill-code-rule/update
   * @secure
   */
  update = (data: BillCodeRuleEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/bill-code-rule/update`,
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
   * @tags bill-code-rule
   * @name Delete
   * @request DELETE:/api/admin/bill-code-rule/delete
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
      path: `/api/admin/bill-code-rule/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags bill-code-rule
   * @name BatchDelete
   * @request PUT:/api/admin/bill-code-rule/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/bill-code-rule/batch-delete`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
}
