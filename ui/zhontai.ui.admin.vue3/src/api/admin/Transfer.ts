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
  PageInputTransferEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputTransferEntity,
  ResultOutputTransferEntity,
  TransferEntity,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class TransferApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags transfer
   * @name GetPage
   * @request POST:/api/admin/transfer/get-page
   * @secure
   */
  getPage = (data: PageInputTransferEntity, params: RequestParams = {}) =>
    this.request<ResultOutputPageOutputTransferEntity, any>({
      path: `/api/admin/transfer/get-page`,
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
   * @tags transfer
   * @name Get
   * @request GET:/api/admin/transfer/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputTransferEntity, any>({
      path: `/api/admin/transfer/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags transfer
   * @name Add
   * @request POST:/api/admin/transfer/add
   * @secure
   */
  add = (data: TransferEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/transfer/add`,
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
   * @tags transfer
   * @name Update
   * @request PUT:/api/admin/transfer/update
   * @secure
   */
  update = (data: TransferEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/transfer/update`,
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
   * @tags transfer
   * @name Delete
   * @request DELETE:/api/admin/transfer/delete
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
      path: `/api/admin/transfer/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags transfer
   * @name Audit
   * @summary 审核调拨单 — 调出仓库减库存 / 调入仓库加库存
   * @request PUT:/api/admin/transfer/audit
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
      path: `/api/admin/transfer/audit`,
      method: 'PUT',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
}
