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
  AccountEntity,
  DynamicFilterInfo,
  DynamicFilterLogic,
  DynamicFilterOperator,
  ResultOutputAccountEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputAccountEntity,
  SortInput,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class AccountApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags account
   * @name GetPage
   * @request GET:/api/admin/account/get-page
   * @secure
   */
  getPage = (
    query: {
      'Filter.Code': string
      'Filter.Name': string
      /** 账户类型：Cash-现金, Bank-银行, Alipay-支付宝, Wechat-微信 */
      'Filter.AccountType'?: string
      /**
       * 期初余额
       * @format double
       */
      'Filter.InitBalance'?: number
      'Filter.Remark'?: string
      'Filter.IsEnabled'?: boolean
      /**
       * 主键Id
       * @format int64
       */
      'Filter.Id'?: number
      /**
       * 创建时间
       * @format date-time
       */
      'Filter.CreatedTime'?: string
      /**
       * 创建者用户Id
       * @format int64
       */
      'Filter.CreatedUserId'?: number
      /** 创建者用户名 */
      'Filter.CreatedUserName'?: string
      /**
       * 修改时间
       * @format date-time
       */
      'Filter.ModifiedTime'?: string
      /**
       * 修改者用户Id
       * @format int64
       */
      'Filter.ModifiedUserId'?: number
      /** 修改者用户名 */
      'Filter.ModifiedUserName'?: string
      /**
       * 当前页标
       * @format int32
       */
      CurrentPage?: number
      /**
       * 每页大小
       * @format int32
       */
      PageSize?: number
      'DynamicFilter.Field'?: string
      /** Contains=0,StartsWith=1,EndsWith=2,NotContains=3,NotStartsWith=4,NotEndsWith=5,Equal=6,Equals=7,Eq=8,NotEqual=9,GreaterThan=10,GreaterThanOrEqual=11,LessThan=12,LessThanOrEqual=13,Range=14,DateRange=15,Any=16,NotAny=17,Custom=18 */
      'DynamicFilter.Operator'?: DynamicFilterOperator
      'DynamicFilter.Value'?: any
      /** And=0,Or=1 */
      'DynamicFilter.Logic'?: DynamicFilterLogic
      'DynamicFilter.Filters'?: DynamicFilterInfo[]
      /** 排序列表 */
      SortList?: SortInput[]
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputPageOutputAccountEntity, any>({
      path: `/api/admin/account/get-page`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags account
   * @name Get
   * @request GET:/api/admin/account/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputAccountEntity, any>({
      path: `/api/admin/account/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags account
   * @name Add
   * @request POST:/api/admin/account/add
   * @secure
   */
  add = (data: AccountEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/account/add`,
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
   * @tags account
   * @name Update
   * @request PUT:/api/admin/account/update
   * @secure
   */
  update = (data: AccountEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/account/update`,
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
   * @tags account
   * @name Delete
   * @summary 删除账户（软删除）
   * @request DELETE:/api/admin/account/delete
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
      path: `/api/admin/account/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags account
   * @name BatchDelete
   * @summary 批量删除账户（软删除）
   * @request PUT:/api/admin/account/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/account/batch-delete`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
}
