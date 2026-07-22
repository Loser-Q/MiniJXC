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
  ContactEntity,
  DynamicFilterInfo,
  DynamicFilterLogic,
  DynamicFilterOperator,
  ResultOutputContactEntity,
  ResultOutputInt64,
  ResultOutputObject,
  ResultOutputPageOutputContactEntity,
  SortInput,
} from './data-contracts'
import { ContentType, HttpClient, RequestParams } from './http-client'

export class ContactApi<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags contact
   * @name GetPage
   * @request GET:/api/admin/contact/get-page
   * @secure
   */
  getPage = (
    query: {
      /** 单位编号 */
      'Filter.Code': string
      /** 单位名称 */
      'Filter.Name': string
      /** 类型：Customer-客户, Supplier-供应商 */
      'Filter.Type': string
      /** 联系人 */
      'Filter.ContactPerson'?: string
      /** 联系电话 */
      'Filter.Phone'?: string
      /** 地址 */
      'Filter.Address'?: string
      /** 备注 */
      'Filter.Remark'?: string
      /**
       * 期初应付款（供应商）
       * @format double
       */
      'Filter.InitPayable'?: number
      /**
       * 期初应收款（客户）
       * @format double
       */
      'Filter.InitReceivable'?: number
      /** 是否启用 */
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
    this.request<ResultOutputPageOutputContactEntity, any>({
      path: `/api/admin/contact/get-page`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags contact
   * @name Get
   * @request GET:/api/admin/contact/get
   * @secure
   */
  get = (
    query?: {
      /** @format int64 */
      id?: number
    },
    params: RequestParams = {}
  ) =>
    this.request<ResultOutputContactEntity, any>({
      path: `/api/admin/contact/get`,
      method: 'GET',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags contact
   * @name Add
   * @request POST:/api/admin/contact/add
   * @secure
   */
  add = (data: ContactEntity, params: RequestParams = {}) =>
    this.request<ResultOutputInt64, any>({
      path: `/api/admin/contact/add`,
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
   * @tags contact
   * @name Update
   * @request PUT:/api/admin/contact/update
   * @secure
   */
  update = (data: ContactEntity, params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/contact/update`,
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
   * @tags contact
   * @name Delete
   * @summary 删除往来单位（软删除）
   * @request DELETE:/api/admin/contact/delete
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
      path: `/api/admin/contact/delete`,
      method: 'DELETE',
      query: query,
      secure: true,
      format: 'json',
      ...params,
    })
  /**
   * No description
   *
   * @tags contact
   * @name BatchDelete
   * @summary 批量删除往来单位（软删除）
   * @request PUT:/api/admin/contact/batch-delete
   * @secure
   */
  batchDelete = (data: number[], params: RequestParams = {}) =>
    this.request<ResultOutputObject, any>({
      path: `/api/admin/contact/batch-delete`,
      method: 'PUT',
      body: data,
      secure: true,
      type: ContentType.Json,
      format: 'json',
      ...params,
    })
}
